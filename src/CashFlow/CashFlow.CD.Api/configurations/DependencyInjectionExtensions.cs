using CashFlow.CD.API.Service;
using CashFlow.CD.Domain.domain;
using CashFlow.CD.Domain.repository;
using CashFlow.CD.Infra.repository;
using CashFlow.RC.Domain.domain;

namespace CashFlow.CD.Api.configurations
{
    public static class DependencyInjectionExtensions
    {
        public static void AddFinancialReportService(this IServiceCollection services)
        {
            services.AddScoped<IFinancialReportService,FinancialReportService>();
        }

        public static IServiceCollection AddFinancialReportConsolidateDaily(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDB");
            services.AddSingleton<IEntityRepository<FinancialReportConsolidateDaily>, EntityRepository<FinancialReportConsolidateDaily>>(
                s => new EntityRepository<FinancialReportConsolidateDaily>(connectionString, "cashflow_report", nameof(FinancialReportConsolidateDaily)));
            return services;
        }
    }
}
