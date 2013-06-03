namespace CfBus
{
    public class ServiceActivator : IServiceActivator
    {
        public object GetInstance(string service)
        {
            return new object();
        }
    }
}