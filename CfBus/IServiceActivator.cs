namespace CfBus
{
    public interface IServiceActivator
    {
        object GetInstance(string service);
    }
}