using System;
using System.Collections.Generic;

namespace CfBus
{
    internal class HostList : IHostList
    {
        public HostList()
        {
            BusinessList = new Dictionary<Type, object>();
        }

        public IDictionary<Type, object> BusinessList { get; set; }
    }
}
