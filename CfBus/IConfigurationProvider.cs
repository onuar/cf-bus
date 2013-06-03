namespace CfBus
{
    public interface IConfigurationProvider
    {
        string BaseAddress { get; set; }

        string QueuePath { get; set; }
    }
}