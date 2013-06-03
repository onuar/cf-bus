using System;

namespace CfBus
{
    public class MethodNotFoundException : Exception
    {
        public MethodNotFoundException(string methodName)
            : base(methodName)
        {
        }
    }
}