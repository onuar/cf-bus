using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CfBus
{
    using System;

    public class CfHandler : HttpMessageHandler, ICfHandler
    {
        private readonly IServiceActivator _activator;
        private readonly IServiceInvoker _invoker;
        private readonly ILogger _logger;
        private readonly IQueue _queue;

        private IHostList _hostList;

        private HttpSelfHostConfiguration _selfHostConfiguration;
        private string _queuePath;

        public CfHandler(IServiceActivator activator, IServiceInvoker invoker, ILogger logger, IQueue queue)
        {
            _activator = activator;
            _invoker = invoker;
            _logger = logger;
            _queue = queue;
        }

        public Task Initialize(IHostList hostList)
        {
            _hostList = hostList;

            _queue.CreateIfIsNotExist(_queuePath);

            var server = new HttpSelfHostServer(_selfHostConfiguration, this);
            return server.OpenAsync();
        }

        public void Configure(IConfigurationProvider configurationProvider)
        {
            _queuePath = configurationProvider.QueuePath;
            _selfHostConfiguration = new HttpSelfHostConfiguration(configurationProvider.BaseAddress);
        }

        public void ListenQueue()
        {
            Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        object queueMessage = _queue.Get();
                        if (queueMessage == null)
                        {
                            continue;
                        }


                        var processComplete = ProcessRequest(queueMessage);
                        if (processComplete)
                        {
                            _queue.DeleteLastMessage();
                        }
                    }
                });
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            #region 1. attempt
            //object requestContent = null;
            //request.Content.ReadAsByteArrayAsync().ContinueWith(task =>
            //    {
            //        var memoryStream = new MemoryStream();
            //        memoryStream.Write(task.Result, 0, task.Result.Length);
            //        var binaryFormatter = new BinaryFormatter();
            //        requestContent = binaryFormatter.Deserialize(memoryStream);

            //    });
            #endregion

            #region 2. attempt (this works)
            //string requestText = string.Empty;
            //Task<string> textTask = request.Content.ReadAsStringAsync();
            //textTask.Wait();
            //requestText = textTask.Result;
            #endregion

            Task<byte[]> requestTask = request.Content.ReadAsByteArrayAsync();
            requestTask.Wait();
            var memoryStream = new MemoryStream();
            memoryStream.Write(requestTask.Result, 0, requestTask.Result.Length);
            memoryStream.Position = 0;
            var binaryFormatter = new BinaryFormatter();

            var cfMessage = binaryFormatter.Deserialize(memoryStream) as CfBusMessage;

            _queue.Send(cfMessage);

            return new Task<HttpResponseMessage>(() => null);
        }

        protected virtual bool ProcessRequest(object message)
        {
            try
            {
                var cfMessage = (CfBusMessage)message;
                var bussinessObject = _hostList.BusinessList.FirstOrDefault();
                var result = _invoker.Invoke(bussinessObject.Value, cfMessage.Method, cfMessage.Arguments);
                return true;
            }
            catch (Exception)
            {
                throw;
            }

            #region junk

            //object serviceInstance = _activator.GetInstance(message.Service);
            ////log
            //object result = _invoker.Invoke(serviceInstance, message.MethodName, message.Arguments);
            ////log

            #endregion
        }
    }
}