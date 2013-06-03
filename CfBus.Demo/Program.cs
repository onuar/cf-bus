using System;
using CfBus.Starbucks.Messages;

namespace CfBus.CashierApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            IServiceActivator activator = new ServiceActivator();
            IServiceInvoker invoker = new ServiceInvoker();
            IQueue queue = new DefaultQueue();
            ICfHandler handler = new CfHandler(activator, invoker, logger, queue);

            IConfigurationProvider configurationProvider = new DefaultConfigurationProvider();
            configurationProvider.BaseAddress = "http://localhost:8089";
            configurationProvider.QueuePath = "CashierMessageQueue";

            ICfBus bus = new CfServiceBus(handler, logger, configurationProvider);
            bus.Host<ICashierService>(new CashierService());
            bus.Start();

            logger.Write("Cashier service has started");
            Console.ReadLine();
        }
    }

    internal class CashierService : ICashierService
    {
        public PaymentDetail Payment(OrderDetail orderDetail)
        {
            Console.WriteLine("Sipariş geldi - Coffee: " + orderDetail.Coffee);
            return new PaymentDetail { Cost = 8 };
        }
    }

    internal class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
