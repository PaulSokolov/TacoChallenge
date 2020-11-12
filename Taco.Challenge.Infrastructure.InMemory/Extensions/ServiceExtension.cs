using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Taco.Challenge.Infrastructure.InMemory.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddInMemoryQueryDispatcherClient(this IServiceCollection services) =>
            services.AddSingleton<IQueryDispatcherClient, QueryDispatcherClient>();
    }
}
