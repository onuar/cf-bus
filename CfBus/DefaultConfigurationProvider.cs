namespace CfBus
{
    public class DefaultConfigurationProvider : IConfigurationProvider
    {
        public string BaseAddress { get; set; }

        public string QueuePath { get; set; }
    }
}