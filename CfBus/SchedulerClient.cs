using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace CfBus
{
    using System.Collections.Generic;

    public class SchedulerClient<TServiceType> : ISchedulerSetup<TServiceType>
    {
        private readonly int _minutes;
        private Timer _timer;
        private Action<TServiceType> _serviceMethod;

        public SchedulerClient(int minutes)
        {
            _minutes = minutes;
        }

        public ISchedulerSetup<TServiceType> Setup(Action<TServiceType> serviceMethod)
        {
            _serviceMethod = serviceMethod;
            
            return this;
        }

        public void Callback(Action<object> callbackAction)
        {
            var list = new List<string>();
            list.Select(x => x);
            _timer = new Timer(new TimerCallback(callbackAction), null, 0, _minutes);
        }
    }
}