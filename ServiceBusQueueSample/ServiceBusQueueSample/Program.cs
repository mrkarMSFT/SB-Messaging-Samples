using Microsoft.ServiceBus.Messaging;
using System;

namespace ServiceBusQueueSample
{
    class Program
    {
        static string connectionString = "Endpoint=sb://<NS>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=";
        static string queueName = "QueueDemo";

        static void Main(string[] args)
        {
            SendMessageToServiceBusQueue();
            //ReceiveMessageFromServiceBusQueue();
        }

        private static void SendMessageToServiceBusQueue()
        {
            var sendClient = QueueClient.CreateFromConnectionString(connectionString, queueName);

            var message = new BrokeredMessage("Queue test message");

            Console.WriteLine(String.Format("Message id: {0}", message.MessageId));

            sendClient.Send(message);

            Console.WriteLine("Message sent successfully! Press ENTER to exit program");
            Console.ReadLine();
        }

        private static void ReceiveMessageFromServiceBusQueue()
        {
            var receiveClient = QueueClient.CreateFromConnectionString(connectionString, queueName);

            receiveClient.OnMessage(message =>
            {
                var clonedMessage = message.Clone();
                Console.WriteLine(String.Format("Message body: {0}", clonedMessage.GetBody<String>()));
                Console.WriteLine(String.Format("Message id: {0}", clonedMessage.MessageId));
            });

            Console.WriteLine("Press ENTER to exit program");
            Console.ReadLine();
        }
    }
}
