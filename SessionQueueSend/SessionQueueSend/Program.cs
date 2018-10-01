using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionQueueSend
{
    class Program
    {
        static string connectionString = "Endpoint=sb://<NS>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=";
        static string queueName = "sessionaware";

        static void Main(string[] args)
        {
            SendMessage().GetAwaiter().GetResult();
        }

        static async Task SendMessage()
        {
            var factory = MessagingFactory.CreateFromConnectionString(connectionString);
            var sender = await factory.CreateMessageSenderAsync(queueName);
            
            for (int i = 0; i < 4000; i++)
            {
                await sender.SendAsync(new BrokeredMessage("Hello World - " + i) { MessageId = i.ToString(), SessionId = "Order" });
                Console.WriteLine(i + " - Message Sent");
            }
            Console.ReadLine();
        }
    }
}
