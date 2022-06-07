using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    public class MessageBusSubscriber
    {
        public MessageBusSubscriber()
        {
            initializeRabbitMQ();
        }

        IConnection connection;
        IModel model;
        string queueName;
        private void initializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672
            };

            connection = factory.CreateConnection();
            model = connection.CreateModel();
            model.ExchangeDeclare("sampleEvents", type: ExchangeType.Fanout);
            queueName = model.QueueDeclare().QueueName;
            model.QueueBind(queue: queueName, exchange: "sampleEvents", routingKey: "custom1");
            Console.WriteLine("RabbitMQ dinleniyor....");

            connection.ConnectionShutdown += Connection_ConnectionShutdown;

           
        }

        public void Execute()
        {
            var consumer = new EventingBasicConsumer(model);
            consumer.Received += Consumer_Received;
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine($"mesaj {e.Exchange} üzerinden alındı ");
            var messageBody = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine(messageBody);
        }

        private void Connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("baplantı kapandı");
        }
    }
}
