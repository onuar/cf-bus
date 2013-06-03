using System.Messaging;

namespace CfBus
{
    public class DefaultQueue : IQueue
    {
        private MessageQueue _messageQueue;

        public void CreateIfIsNotExist(string path)
        {
            if (!path.Contains(@".\Private$\"))
            {
                path = @".\Private$\" + path;
            }

            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }

            _messageQueue = new MessageQueue(path);
            _messageQueue.Formatter = new BinaryMessageFormatter();
        }

        public void DeleteQueue(string path)
        {
            MessageQueue.Delete(path);
        }

        public void Send(object message)
        {
            var queueMessage = new Message
                {
                    Label = "CfMessage",
                    Body = message,
                    Formatter = new BinaryMessageFormatter()
                };
            _messageQueue.Send(queueMessage);
        }

        public object Get()
        {
            var queueMessage = (Message)_messageQueue.Peek();
            return queueMessage.Body;
        }

        public object GetAndDelete()
        {
            return _messageQueue.Receive();
        }

        public void DeleteLastMessage()
        {
            _messageQueue.Receive();
        }
    }
}
