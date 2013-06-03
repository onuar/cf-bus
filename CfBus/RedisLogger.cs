using ServiceStack.Redis;

namespace CfBus
{
    using System;

    public class RedisLogger : ILogger
    {
        private readonly Uri _uri;

        public RedisLogger(Uri uri)
        {
            _uri = uri;
        }

        public void Write(string message)
        {
            using (var redis = new RedisClient(_uri))
            {
                redis.Add("CfBus log", message);
            }
        }
    }
}
