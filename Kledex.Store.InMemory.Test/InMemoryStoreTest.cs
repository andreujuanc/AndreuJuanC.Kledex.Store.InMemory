using Moq;
using Kledex.Bus;
using Kledex.Commands;
using Kledex.Domain;
using Kledex.Events;
using Kledex.Queries;
using System;
using System.Threading.Tasks;
using Xunit;
using Kledex.Examples.Domain;

namespace Kledex.Store.InMemory.Test
{
    public class InMemoryStoreTest
    {
        readonly InMemoryStore sut = null;
        public InMemoryStoreTest()
        {
            var versionService = new VersionService();
            sut = new InMemoryStore(versionService);
        }

        [Fact]
        public async Task SaveAggregate()
        {
            var id = Guid.NewGuid();
            Assert.Empty(sut.GetAggregates());
            await sut.SaveAggregateAsync<Product>(id);
            var list = sut.GetAggregates();
            Assert.Contains(list, x => x.Id == id);
        }
    }
}
