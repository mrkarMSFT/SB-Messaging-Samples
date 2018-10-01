using Microsoft.ServiceBus.Messaging;
using System;

namespace ServiceBusTopicSample
{
    class Program
    {
        static string connectionString = "Endpoint=sb://<NS>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=";
        static string topicName = "TopicDemo";

        static string subscriptionName = "SubscriptionDemo";

        static void Main(string[] args)
        {
            SendMessageToServiceBusTopic();
            ReceiveMessageFromServiceBusTopic();
        }

        private static void SendMessageToServiceBusTopic()
        {
            var sendClient = TopicClient.CreateFromConnectionString(connectionString, topicName);
            var message = new BrokeredMessage("Topic test message");

            Console.WriteLine(String.Format("Message id: {0}", message.MessageId));

            sendClient.Send(message);

            Console.WriteLine("Message successfully sent! Press ENTER to exit program");
            Console.ReadLine();
        }

        private static void ReceiveMessageFromServiceBusTopic()
        {
            var receiveClient = SubscriptionClient.CreateFromConnectionString(connectionString, topicName, subscriptionName);

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
