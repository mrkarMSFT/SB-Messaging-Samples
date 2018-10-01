using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.IO;

namespace ServiceBusReadFromDeadLetter
{
    class Program
    {
        static void Main(string[] args)
        {
            RetrieveMessageFromDeadLetterForQueue();
            RetrieveMessageFromDeadLetterForSubscription();
        }

        public static void RetrieveMessageFromDeadLetterForQueue()
        {
            var receiverFactory = MessagingFactory.Create(
                 "sb://<NS>.servicebus.windows.net/",
                 new MessagingFactorySettings
                 {
                     TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", ""),
                     NetMessagingTransportSettings = { BatchFlushInterval = new TimeSpan(0, 0, 0) }
                 });

            string data = QueueClient.FormatDeadLetterPath("queuedemo");

            var receiver = receiverFactory.CreateMessageReceiver(data);

            receiver.OnMessageAsync(
            async message =>
            {
                var body = message.GetBody<Stream>();

                lock (Console.Out)
                {
                    Console.WriteLine(message.MessageId);
                }

                await message.CompleteAsync();
            },
            new OnMessageOptions { AutoComplete = false, MaxConcurrentCalls = 1 });
        }

        public static void RetrieveMessageFromDeadLetterForSubscription()
        {
            var receiverFactory = MessagingFactory.Create(
                 "sb://<NS>.servicebus.windows.net/",
                 new MessagingFactorySettings
                 {
                     TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", ""),
                     NetMessagingTransportSettings = { BatchFlushInterval = new TimeSpan(0, 0, 0) }
                 });

            string data = SubscriptionClient.FormatDeadLetterPath("mrkartopic", "mrkarsub1");

            var receiver = receiverFactory.CreateMessageReceiver(data);

            receiver.OnMessageAsync(
            async message =>
            {
                var body = message.GetBody<Stream>();

                lock (Console.Out)
                {
                    Console.WriteLine("Message ID :" + message.MessageId);
                }

                await message.CompleteAsync();
            },
            new OnMessageOptions { AutoComplete = false, MaxConcurrentCalls = 1 });
        }
    }
}
