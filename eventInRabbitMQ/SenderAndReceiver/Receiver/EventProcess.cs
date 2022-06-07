using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Receiver
{
    public class EventProcess : IEventProcess
    {
        public EventProcess()
        {
                
        }

        
        public void ProcessEvent(string message)
        {
            var eventType = determineEvent(message);
        }

        private EventType determineEvent(string message)
        {
            // dynamic eventType = JsonSerializer.Deserialize<object>(message);
            return EventType.SampleEvent;
        }
    }

    public enum EventType
    {
        SampleEvent,
        Undetermined

    }
}
