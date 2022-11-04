using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Subscriber
{
    internal class Program
    {
        private const string RedisConnectionString = "127.0.0.1:6379";
        private static ConnectionMultiplexer connection =
                        ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "message-ImportExcel";
        static void Main(string[] args)
        {
            Console.WriteLine("Listening message-ImportExcel");
            var pubsub = connection.GetSubscriber();

            pubsub.Subscribe(Channel, (channel, message) => Console.WriteLine
                        ("Message received from message-ImportExcel : " + message));

            Console.ReadLine();
        }
    }
}
