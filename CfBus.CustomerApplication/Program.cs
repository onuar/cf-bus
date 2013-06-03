using System;
using CfBus.Starbucks.Messages;

namespace CfBus.CustomerApplication
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
            configurationProvider.BaseAddress = "http://localhost:8088";
            configurationProvider.QueuePath = "CustumerMessageQueue";
            ICfBus bus = new CfServiceBus(handler, logger, configurationProvider);
            bus.Host<ICustomerService>(new CustomerService(new CashierProxy("http://localhost:8089")));
            bus.Start();

            logger.Write("Customer cashierService has started");
            Console.ReadLine();
        }

        internal class CustomerService : ICustomerService
        {
            private readonly ICashierService _cashierService;

            public CustomerService(ICashierService cashierService)
            {
                _cashierService = cashierService;
            }

            public void Order(string customerName, OrderDetail orderDetail)
            {
                _cashierService.Payment(orderDetail);
            }
        }

        internal class CashierProxy : ServiceProxyBase<ICashierService>, ICashierService
        {
            public CashierProxy(string baseAddress)
                : base(baseAddress)
            {
            }

            public PaymentDetail Payment(OrderDetail orderDetail)
            {
                return Channel.Payment(orderDetail);
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
}
