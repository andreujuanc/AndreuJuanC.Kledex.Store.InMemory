using Kledex.Commands;
using Kledex.Domain;
using Kledex.Examples.Domain;
using Kledex.Examples.Domain.Commands;
using Kledex.Examples.Reporting;
using Kledex.Examples.Reporting.Queries;
using Kledex.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kledex.Store.InMemory.Test
{
    public class DispatcherTests
    {

        readonly InMemoryStore SUT;
        readonly IDispatcher Dispatcher;
        public DispatcherTests()
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
                   {
                        {"DomainDbConfiguration:ConnectionString", "MyDatabase"},
                   })
               .Build();

            var builder = new ServiceCollection()
                 //.AddLogging()
                 ;

            builder
               .AddKledex(typeof(Product))
                 .AddInMemoryProvider(config)
                 .AddOptions((o) => { });
                ;
            var serviceProvider = builder.BuildServiceProvider();
            Dispatcher = serviceProvider.GetService<IDispatcher>();
            SUT = serviceProvider.GetService<ICommandStore>() as InMemoryStore;

        }

        [Fact]
        public async Task CRUDAggregate()
        {
            var id = Guid.NewGuid();
            Assert.Empty(SUT.GetAggregates());

            await Dispatcher.SendAsync< CreateProduct, Product>(new CreateProduct() {  AggregateRootId = id, Title="Le title" });
            await Dispatcher.GetResultAsync< GetProduct, ProductViewModel>(new GetProduct() { Id = id });
            var list = SUT.GetAggregates();
            Assert.NotEmpty (list);
        }
    }
}
