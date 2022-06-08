using EventBus.Base;
using EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace EventBus.Test
{

   
    public class UnitTest1
    {

        private ServiceCollection services;
        public UnitTest1()
        {
            services = new ServiceCollection();
           
        }
        
     
        [Fact]
        public void subcribe_event()
        {
            services.AddSingleton<IEventBus>(sp => {
                var config = GetEventBusConfig();
                return new EventBusRabbitMQ(config, sp);
            });

            var serviceProvider = services.BuildServiceProvider();

            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            //eventBus.Publish(new OrderCreatedIntegrationEvent(1,1,new List<OrderItem> ()));

            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedEventHandler>();
            
            
        }

        [Fact]
        public void publish_event()
        {
            services.AddSingleton<IEventBus>(sp => {
                var config = GetEventBusConfig();
                return new EventBusRabbitMQ(config, sp);
            });
            var serviceProvider = services.BuildServiceProvider();
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.Publish(new OrderCreatedIntegrationEvent(1, "turkay", new List<OrderItem>()));

        }

        [Fact]
        public void conume_event()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                var config = GetEventBusConfig();
                return new EventBusRabbitMQ(config, sp);
            });
            var serviceProvider = services.BuildServiceProvider();
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedEventHandler>();
        }

        private EventBusConfig GetEventBusConfig()
        {
            return new EventBusConfig();
        }

        
    }
}