using System;
using System.Reflection;

namespace CfBus
{
    [Serializable]
    public class CfBusMessage
    {
        public object[] Arguments { get; set; }

        public MethodInfo Method { get; set; }
    }
}