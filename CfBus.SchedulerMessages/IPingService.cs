using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfBus.SchedulerMessages
{
    public interface IPingService
    {
        bool Ping(string machineName, DateTime liveDate);
    }
}
