namespace CfBus.SchedulerClientApp
{
    using System;

    using CfBus.SchedulerMessages;

    class Program
    {
        static void Main(string[] args)
        {
            // todo@onuar: öylesine bir proxy denemesi yaptım. host çalışıyor mu diye baktım.
            // var proxy = new PingProxyClient("http://localhost:8090");
            // proxy.Ping("denem", DateTime.Now);

            var schedulerClient = new SchedulerClient<IPingService>(10);
            schedulerClient
                .Setup(service => service.Ping("SCHParam", DateTime.Now))
                .Callback(o => Console.WriteLine("Callback: {0}", DateTime.Now));

            Console.ReadLine();
        }
    }
}
