using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kledex.Bus;
using Kledex.Dependencies;
using Kledex.Domain;
using Kledex.Extensions;
using System;

namespace Kledex.Store.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IKledexServiceBuilder AddInMemoryProvider(this IKledexServiceBuilder builder, IConfiguration configuration)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var store = new InMemoryStore(new VersionService());// todo
            var busdistpacher = new InMemoryBusMessageDispatcher();

            builder.Services
              .AddTransient<IAggregateStore>((sp) => store)
              .AddTransient<ICommandStore>((sp) => store)
              .AddTransient<IEventStore>((sp) => store);

            builder.Services.AddSingleton<IBusMessageDispatcher>((sp) => busdistpacher);
            builder.Services.AddSingleton<IResolver, Resolver>();
            

            builder.Services.Scan(s => s
                .FromAssembliesOf(typeof(InMemoryStore))
                .AddClasses()
                .AsImplementedInterfaces());

            return builder;
        }
    }
}
