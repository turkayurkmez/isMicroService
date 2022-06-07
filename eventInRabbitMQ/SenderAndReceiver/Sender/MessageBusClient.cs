using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sender
{
    public class MessageBusClient : IMessageBusClient, IDisposable
    {
        IConnection connection;
        IModel channel;
        public MessageBusClient()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
            };

            try
            {
                connection = connectionFactory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: "sampleEvents", type: ExchangeType.Fanout);
                connection.ConnectionShutdown += (sender, e) =>
                {
                    Console.WriteLine("Connection is shutdown");
                };
            }
            catch (Exception x)
            {

                Console.WriteLine($"Bir hata oluştu: {x.Message}");
            }
        
        }

        public void Dispose()
        {
            if (channel.IsOpen)
            {
                channel.Close();
                connection.Close();
            }
            Console.WriteLine("Disposed...");
        }

        public void Send(ProductDto productDto)
        {
            var jsonMessage = JsonSerializer.Serialize(productDto);
            if (connection.IsOpen)
            {
                sendMessage(jsonMessage);
            }
            else
            {
                Console.WriteLine("Bağlantı kapalı...");
            }
        }

        private void sendMessage(string jsonMessage)
        {
            var messageBody = Encoding.UTF8.GetBytes(jsonMessage);
            channel.BasicPublish(exchange: "sampleEvents", routingKey: "custom1", basicProperties: null, body: messageBody);
            Console.WriteLine("Mesaj Gönderildi....");

        }
    }
}
