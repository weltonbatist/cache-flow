using CashFlow.CD.Domain.domain;
using CashFlow.CD.Domain.repository;
using CashFlow.CD.Infra.repository;
using CashFlow.CD.Work.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.CD.Work.extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services)
        {
            services.AddSingleton<PersistenceService>();
            return services;
        }

        public static IServiceCollection AddFinancialReportConsolidateDailyRepository(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDB");
            services.AddSingleton<IEntityRepository<FinancialReportConsolidateDaily>, EntityRepository<FinancialReportConsolidateDaily>>(
                s => new EntityRepository<FinancialReportConsolidateDaily>(connectionString, "cashflow_report", nameof(FinancialReportConsolidateDaily)));
            return services;
        }


    }
}
