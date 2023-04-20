using CashFlow.NATS.configure;
using CashFlow.RC.Domain.domain;
using CashFlow.RC.Domain.repository;
using CashFlow.RC.Infra.repository;
using CashFlow.RC.Work.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.Work.extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services)
        {
            services.AddSingleton<PersistenceService>();
            return services;
        }

        public static IServiceCollection AddFinancialReleaseRepository(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDB");
            services.AddSingleton<IEntityRepository<FinancialRelease>, EntityRepository<FinancialRelease>>(
                s => new EntityRepository<FinancialRelease>(connectionString,"cashflow",nameof(FinancialRelease)));
            return services;
        }

       
    }
}
