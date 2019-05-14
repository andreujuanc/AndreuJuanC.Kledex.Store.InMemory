using Moq;
using OpenCqrs.Bus;
using OpenCqrs.Commands;
using OpenCqrs.Domain;
using OpenCqrs.Events;
using OpenCqrs.Queries;
using OpenCqrs.Tests.Fakes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OpenCqrs.Store.InMemory.Test
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
