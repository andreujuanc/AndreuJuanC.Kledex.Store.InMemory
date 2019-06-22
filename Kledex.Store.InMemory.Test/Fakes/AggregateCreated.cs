using System;
using System.Collections.Generic;
using Kledex.Bus;
using Kledex.Domain;

namespace Kledex.Tests.Fakes
{
    public class AggregateCreated : DomainEvent, IBusQueueMessage
    {
        public DateTime? ScheduledEnqueueTimeUtc { get; set; }
        public string QueueName { get; set; } = "queue-name";
        public IDictionary<string, object> Properties { get; set; }
    }
}
