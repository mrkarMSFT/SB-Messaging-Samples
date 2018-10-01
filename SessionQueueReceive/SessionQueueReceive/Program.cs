using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionQueueReceive
{
    class Program
    {
        static string connectionString = "Endpoint=sb://<NS>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=";
        static string queueName = "sessionaware";

        static void Main(string[] args)
        {
            //Queue receive without Batching 

            var factory = MessagingFactory.CreateFromConnectionString(connectionString);
            var receiver = factory.CreateQueueClient(queueName).AcceptMessageSession(true);

            while (true)
            {
                var msg = receiver.ReceiveAsync().GetAwaiter().GetResult();
                Console.WriteLine("Receiving message - {0}", msg.MessageId);
                msg.CompleteAsync();
            }


            //Queue receive with Batching 

            var factory = MessagingFactory.CreateFromConnectionString(connectionString);
            var receiver = factory.CreateQueueClient(queueName).AcceptMessageSession(true);

            while (true)
            {
                var msg = receiver.ReceiveBatchAsync(10).GetAwaiter().GetResult();
                foreach (var m in msg)
                {
                    Console.WriteLine("Receiving message - {0}", m.MessageId);
                    m.CompleteAsync();
                }
            }
        }
    }
}
