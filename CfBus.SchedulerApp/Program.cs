using System;

namespace CfBus.SchedulerApp
{
    using CfBus.SchedulerMessages;

    class Program
    {
        static void Main(string[] args)
        {
            var activator = new ServiceActivator();
            var invoker = new ServiceInvoker();
            var logger = new ConsoleLogger();
            var queue = new DefaultQueue();

            var handler = new CfHandler(activator, invoker, logger, queue);

            var configurationProvider = new DefaultConfigurationProvider();
            configurationProvider.BaseAddress = "http://localhost:8090";
            configurationProvider.QueuePath = "SchedulerQueue";
            var bus = new CfServiceBus(handler, logger, configurationProvider);
            bus.Host<IPingService>(new PingService());
            bus.Start();
            Console.ReadLine();
        }

        public class PingService : IPingService
        {
            public bool Ping(string machineName, DateTime liveDate)
            {
                Console.WriteLine("{0} is alive at {1}", machineName, liveDate.ToString());
                return true;
            }
        }

        public class ConsoleLogger : ILogger
        {
            public void Write(string message)
            {
                Console.WriteLine(message);
            }
        }
    }
}
