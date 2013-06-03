using System;
using System.Threading.Tasks;

namespace CfBus
{
    public class CfServiceBus : ICfBus
    {
        private readonly IConfigurationProvider _configuration;
        private readonly ICfHandler _handler;
        private readonly ILogger _logger;
        private readonly IHostList _hostList;
        private Task _handlerTask;

        public CfServiceBus(ICfHandler handler, ILogger logger, IConfigurationProvider configuration)
        {
            _configuration = configuration;
            _handler = handler;
            _logger = logger;

            _hostList = new HostList();
        }

        public void Start()
        {
            _handler.Configure(_configuration);
            _handlerTask = _handler.Initialize(_hostList);
            _handlerTask.Wait();
            _handlerTask.ContinueWith(task => _handler.ListenQueue());
            _logger.Write("Service has started...");
        }

        public void Host<TBusinessContract>(TBusinessContract businessInstance)
        {
            Type type = typeof(TBusinessContract);
            _hostList.BusinessList.Add(type, businessInstance);
        }
    }
}