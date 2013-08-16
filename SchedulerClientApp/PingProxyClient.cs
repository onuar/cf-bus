using System;
using CfBus.SchedulerMessages;

namespace CfBus.SchedulerClientApp
{
    public class PingProxyClient : ServiceProxyBase<IPingService>, IPingService
    {
        public PingProxyClient(string baseAddress)
            : base(baseAddress)
        {
        }

        public bool Ping(string machineName, DateTime liveDate)
        {
            return Channel.Ping(machineName, liveDate);
        }
    }
}
