using System;
using System.Collections.Generic;

namespace CfBus
{
    public interface IHostList
    {
        IDictionary<Type, object> BusinessList { get; set; }
    }
}
