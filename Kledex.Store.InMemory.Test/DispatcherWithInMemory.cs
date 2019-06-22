using Moq;
using Kledex.Bus;
using Kledex.Commands;
using Kledex.Domain;
using Kledex.Events;
using Kledex.Queries;
using Kledex.Tests.Fakes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kledex.Store.InMemory.Test
{
    public class DispatcherWithInMemory
    {
        readonly InMemoryStore sut = null;
        public DispatcherWithInMemory()
        {
            var versionService = new VersionService();
            sut = new InMemoryStore(versionService);
        }

        [Fact]
        public async Task SaveAggregate()
        {
            var id = Guid.NewGuid();
            Assert.Empty(sut.GetAggregates());
            sut.SaveAggregate< Aggregate>(id);
            var list = sut.GetAggregates();
            Assert.Contains(list, x => x.Id == id);
        }

    }
}
