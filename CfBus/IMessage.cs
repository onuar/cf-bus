namespace CfBus
{
    public interface IMessage
    {
        string MethodName { get; set; }

        object[] Arguments { get; set; }

        string Service { get; set; }
    }
}