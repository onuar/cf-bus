namespace CfBus
{
    public interface IQueue
    {
        void CreateIfIsNotExist(string path);

        void DeleteQueue(string path);

        void Send(object message);

        object Get();

        object GetAndDelete();

        void DeleteLastMessage();
    }
}