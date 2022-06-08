using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base
{
    public class EventBusConfig
    {
        public string EventBusConnectionString { get; set; }
        public string DefaultTopicName { get; set; } = "ShopEventBus";
        public int ConnectionRetryCount { get; set; } = 5;
        public string SubscriberClientName { get; set; }
        public string EventNamePrefix { get; set; }
        public string EventNameSuffix { get; set; } = "IntegrationEvent";
        public bool IsPrefixDeleted { get => !string.IsNullOrEmpty(EventNamePrefix); }
        public bool IsSuffixDeleted { get => !string.IsNullOrEmpty(EventNameSuffix); }
        public object Connection { get; set; }

    }
}
