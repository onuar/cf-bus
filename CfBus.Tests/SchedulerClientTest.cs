using NUnit.Framework;

namespace CfBus.Tests
{
    using System;

    [TestFixture]
    public class SchedulerClientTest
    {
        [TestFixtureSetUp]
        public void InitServer()
        {
            var activator = new ServiceActivator();
            var invoker = new ServiceInvoker();
            var logger = new RedisLogger(new Uri("test"));
            var queue = new DefaultQueue();

            var handler = new CfHandler(activator, invoker, logger, queue);

            var configurationProvider = new DefaultConfigurationProvider();
            configurationProvider.BaseAddress = "http://localhost:8090";
            var bus = new CfServiceBus(handler, logger, configurationProvider);
            bus.Host<IPingPongService>(new PingPongService());
            bus.Start();
        }

        [Test]
        public void TestMethod()
        {
            const string MachineName = "COMP";
            var client = new SchedulerClient<IPingPongService>(10);
            client
                .Setup(c => c.Ping(MachineName, DateTime.Now))
                .Callback(service => Console.WriteLine("Callback: {0} - {1}", MachineName, DateTime.Now));
        }
    }

    public class PingPongService : IPingPongService
    {
        public bool Ping(string machineName, DateTime liveDate)
        {
            Console.WriteLine("{0} is alive at {1}");
            return true;
        }
    }

    public interface IPingPongService
    {
        bool Ping(string machineName, DateTime liveDate);
    }
}
