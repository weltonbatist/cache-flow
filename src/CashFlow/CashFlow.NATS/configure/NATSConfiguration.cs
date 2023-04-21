using CashFlow.NATS.factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.NATS.configure
{
    public static class NATSConfiguration
    {
        public static void AddNATSConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            Server server = new()
            {
                Url = configuration["NATSServer:url"],
            };
            services.AddSingleton<ICreateConnectionFactory, CreateConnectionFactory>();
            services.AddSingleton(server);
            services.AddSingleton<NatsPublisher>();
            services.AddSingleton<NatsSubscriber>();
        }
    }
}
