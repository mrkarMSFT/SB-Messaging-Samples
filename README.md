# SB-Messaging-Samples


Microsoft Azure Service Bus is a fully managed enterprise integration message broker. Service Bus is most commonly used to decouple applications and services from each other, and is a reliable and secure platform for asynchronous data and state transfer. Data is transferred between different applications and services using messages. A message is in binary format, which can contain JSON, XML, or just text.

Some common messaging scenarios are:

- Messaging: transfer business data, such as sales or purchase orders, journals, or inventory movements.
- Decouple applications: improve reliability and scalability of applications and services (client and service do not have to be online at the same time).
- Topics and subscriptions: enable 1:n relationships between publishers and subscribers.
- Message sessions: implement workflows that require message ordering or message deferral.

This repository holds the sample to get started with azure service bus queues and topic/subscription.

You need to have a namespace, a queue, a topic and a subscription under the topic to run the code successfully.
