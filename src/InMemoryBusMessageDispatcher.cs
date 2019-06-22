using Kledex.Bus;
using System;
using System.Threading.Tasks;

namespace Kledex.Store.InMemory
{
    public class InMemoryBusMessageDispatcher : IBusMessageDispatcher
    {
        public Task DispatchAsync<TMessage>(TMessage message) where TMessage : IBusMessage
        {
            throw new NotImplementedException();
        }
    }
}    
