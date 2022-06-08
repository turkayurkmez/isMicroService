using EventBus.Base;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : EventBusBase
    {
        RabbitMQPersistentConnection persistentConnection;
        private readonly IConnectionFactory connectionFactory;
        private readonly IModel consumerModel;
        public EventBusRabbitMQ(EventBusConfig eventBusConfig, IServiceProvider serviceProvider) : base(eventBusConfig, serviceProvider)
        {
            if (eventBusConfig.Connection != null)
            {
                var conectionJson = JsonConvert.SerializeObject(eventBusConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                var connection = JsonConvert.DeserializeObject<ConnectionFactory>(conectionJson);

            }
            else
            {
                connectionFactory = new ConnectionFactory();
            }

            persistentConnection = new RabbitMQPersistentConnection(connectionFactory);
            consumerModel = createConsumeModel();

            this._subsManager.OnEventRemoved += _subsManager_OnEventRemoved;

        }

        private void _subsManager_OnEventRemoved(object? sender, string e)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }
            using (var channel = persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: e, exchange: EventBusConfig.DefaultTopicName, routingKey: e);
                channel.QueueDelete(e);
            }
        }

        private IModel? createConsumeModel()
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var channel = persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");
            return channel;
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(retryCount: EventBusConfig.ConnectionRetryCount,
                    sleepDurationProvider: (retryAttempt) => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        //log....
                    });

            var eventName = @event.GetType().Name;
            eventName = processEventName(eventName);

            consumerModel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            policy.Execute(() =>
            {
                var properties = consumerModel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                consumerModel.BasicPublish(exchange: EventBusConfig.DefaultTopicName,
                     routingKey: eventName,
                     mandatory: true,
                     basicProperties: properties,
                     body: body);
            });



        }

        public override void Subscribe<TEvent, TEventHandler>()
        {
            var eventName = typeof(TEvent).Name;
            eventName = processEventName(eventName);
            if (!_subsManager.HasSubscriptionsForEvent(eventName))
            {
                if (!persistentConnection.IsConnected)
                {
                    persistentConnection.TryConnect();
                }
                
                consumerModel.QueueDeclare(queue: eventName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                consumerModel.QueueBind(queue: eventName, exchange: EventBusConfig.DefaultTopicName, routingKey: eventName);

            }
            _subsManager.AddSubscription<TEvent, TEventHandler>();
            StartBasicConsume(eventName);
        }
        

        public void StartBasicConsume(string eventName)
        {
            if (consumerModel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(consumerModel);
               
                consumerModel.BasicConsume(queue: eventName, autoAck: false, consumer: consumer);
                consumer.Received += Consumer_Received;
            }
            else
            {
                throw new Exception("EventBus RabbitMQ is not initialized.");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var eventName = @event.RoutingKey;
            eventName = processEventName(eventName);
            var message = Encoding.UTF8.GetString(@event.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception)
            {
                
                
            }
            finally
            {
                consumerModel.BasicAck(@event.DeliveryTag, multiple: false);
            }
        }

        public override void Unsubscribe<TEvent, TEventHandler>()
        {
            _subsManager.RemoveSubscription<TEvent, TEventHandler>();
        }
    }
}