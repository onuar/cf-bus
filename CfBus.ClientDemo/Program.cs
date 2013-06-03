using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;

namespace CfBus.ClientDemo
{
    using CfBus.Starbucks.Messages;

    class Program
    {
        static void Main(string[] args)
        {
            #region first attempt
            //var uri = new Uri("http://localhost:8089/ClientTestParameter");

            //var client = new HttpClient();
            ////Task<HttpResponseMessage> responseTask = client.GetAsync(uri);

            //var request = new HttpRequestMessage();
            //request.RequestUri = new Uri("http://localhost:8089/ClientTestParameter"); ;
            //request.Method = new HttpMethod("GET");

            //var arg = new MyArgument { Name = "Onur", Surname = "Aykaç" };
            //var memoryStream = new MemoryStream();
            //var binaryFormatter = new BinaryFormatter();
            //binaryFormatter.Serialize(memoryStream, arg);
            //request.Content = new ByteArrayContent(memoryStream.ToArray());

            //var responseTask = client.PostAsync(uri, request.Content);
            //responseTask.Result.EnsureSuccessStatusCode();

            ////responseTask.Result.EnsureSuccessStatusCode();
            ////Task<string> incomingMessage = responseTask.Result.Content.ReadAsStringAsync();
            ////Console.WriteLine(incomingMessage.Result);

            #endregion

            #region second attempt (this works)
            //var requestMessage = new HttpRequestMessage();
            //requestMessage.Content = new StringContent("Param 1 HttpRequestMessagexXx");
            //requestMessage.RequestUri = new Uri("http://localhost:8089/attempt2");
            //requestMessage.Method = new HttpMethod("POST");

            //var client = new HttpClient();
            //Task<HttpResponseMessage> responseMessage = client.SendAsync(requestMessage);
            //responseMessage.Result.EnsureSuccessStatusCode();
            #endregion

            #region third attempt (this works too)
            //var requestMessage = new HttpRequestMessage();
            //requestMessage.RequestUri = new Uri("http://localhost:8089/attempt3");
            //requestMessage.Method = new HttpMethod("POST");

            //var arg = new MyArgument { Name = "Onur", Surname = "Aykaç" };

            //var message = new CfBusMessage();
            //message.Arguments = new object[1];
            ////message.Arguments[0] = arg;

            //var memoryStream = new MemoryStream();
            //var binaryFormatter = new BinaryFormatter();
            //binaryFormatter.Serialize(memoryStream, message);
            //requestMessage.Content = new ByteArrayContent(memoryStream.ToArray());

            //var client = new HttpClient();
            //Task<HttpResponseMessage> responseMessage = client.SendAsync(requestMessage);
            //responseMessage.Result.EnsureSuccessStatusCode();
            #endregion

            var customerClient = new CustomerProxy("http://localhost:8088");
            customerClient.Order("Onuradam", new OrderDetail { Coffee = "White mocha", Size = 2 });
            Console.ReadLine();
        }
    }

    internal class CustomerProxy : ServiceProxyBase<ICustomerService>, ICustomerService
    {
        public CustomerProxy(string baseAddress)
            : base(baseAddress)
        {

        }

        public void Order(string customerName, OrderDetail orderDetail)
        {
            Channel.Order(customerName, orderDetail);
        }
    }

    [Serializable]
    class MyArgument
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
