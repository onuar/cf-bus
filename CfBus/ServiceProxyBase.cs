using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace CfBus
{
    using System;

    using CodeFiction.Stack.Library.Core.Castle;
    using CodeFiction.Stack.Library.CoreContracts;

    public class ServiceProxyBase<TBusinessType>
        where TBusinessType : class
    {
        private readonly string _baseAddress;

        private TBusinessType _channel;

        protected ServiceProxyBase(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        protected TBusinessType Channel
        {
            get
            {
                Initialize();

                return _channel;
            }
        }

        private void Initialize()
        {
            if (_channel != null)
            {
                return;
            }

            #region WebApi ChannelFactory

            //var endPoint = new EndpointAddress(_baseAddress);
            //var channelFactory = new ChannelFactory<TBusinessType>(new WSHttpBinding());
            //_channel = channelFactory.CreateChannel(endPoint);

            #endregion

            #region Castle ChannelFactory
            var proxyProvider = new CastleDynamicProxyProvider();
            _channel = proxyProvider.Create<TBusinessType>(new FuncInterceptor(SendRequest));
            #endregion
        }

        public object SendRequest(IMethodInvocation invocation)
        {
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(_baseAddress);
            requestMessage.Method = new HttpMethod("POST");

            var message = new CfBusMessage();
            message.Arguments = invocation.Arguments;
            message.Method = invocation.Method;

            var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, message);
            requestMessage.Content = new ByteArrayContent(memoryStream.ToArray());

            var client = new HttpClient();
            Task<HttpResponseMessage> responseMessage = client.SendAsync(requestMessage);
            responseMessage.Result.EnsureSuccessStatusCode();

            return null;
        }

        //private void SendRequest(string methodName, params object[] arguments)
        //{
        //    var requestMessage = new HttpRequestMessage();
        //    requestMessage.RequestUri = new Uri(_baseAddress);
        //    requestMessage.Method = new HttpMethod("POST");

        //    var message = new CfBusMessage();
        //    message.Arguments = arguments;

        //    var memoryStream = new MemoryStream();
        //    var binaryFormatter = new BinaryFormatter();
        //    binaryFormatter.Serialize(memoryStream, message);
        //    requestMessage.Content = new ByteArrayContent(memoryStream.ToArray());

        //    var client = new HttpClient();
        //    Task<HttpResponseMessage> responseMessage = client.SendAsync(requestMessage);
        //    responseMessage.Result.EnsureSuccessStatusCode();
        //}

        //protected void Send(object parameter)
        //{
        //}
    }

    internal class FuncInterceptor : IInterceptor
    {
        private readonly Func<IMethodInvocation, object> _func;

        public FuncInterceptor(Func<IMethodInvocation, object> func)
        {
            _func = func;
        }

        public object Intercept(IMethodInvocation methodInvocation)
        {
            return _func(methodInvocation);
        }
    }
}
