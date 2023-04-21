using CashFlow.NATS.configure;
using CashFlow.RC.API.Service.services;

namespace CashFlow.RC.API.configurations
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FinancialReleaseService>();
            services.AddNATSConfiguration(configuration);
        }
    }
}
