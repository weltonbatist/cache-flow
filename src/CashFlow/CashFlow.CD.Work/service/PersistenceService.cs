using CashFlow.CD.Domain.domain;
using CashFlow.CD.Domain.repository;
using CashFlow.NATS;
using CashFlow.Util.extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using NATS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CashFlow.CD.Work.service
{
    public class PersistenceService
    {
        private readonly ILogger<PersistenceService> _logger;
        private readonly IEntityRepository<FinancialReportConsolidateDaily> _repository;
       
        public PersistenceService(ILogger<PersistenceService> logger, IEntityRepository<FinancialReportConsolidateDaily> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task Process(Msg message)
        {
            string correlationId = string.Empty;

            if (message.HasHeaders)
                correlationId = GetCorrelationId(message);

            if (message.Data is null)
            {
                _logger.LogWarning($"Message received with no data. CorrelationId: {correlationId}");
                return;
            }

            var data = Encoding.UTF8.GetString(message.Data);

            var entity = JsonSerializer.Deserialize<FinancialSummary>(data);

            if (entity is null)
            {
                _logger.LogWarning($"Message received with no entity(FinancialSummary). CorrelationId: {correlationId}");
                return;
            }

            await SaveSummary(entity);
            
            

            _logger.LogInformation($"Registered entity. CorrelationId: {correlationId}");

        }

        private async Task SaveSummary(FinancialSummary entity)
        {
            var dateOnly =DateOnly.FromDateTime(entity.Date);
            var filter = Builders<FinancialReportConsolidateDaily>.Filter.Eq("Date", dateOnly);
            var financialReportConsolidateDaily = await _repository.FindOneAsync(filter);

            if(financialReportConsolidateDaily is null)
            {
                financialReportConsolidateDaily = new FinancialReportConsolidateDaily(dateOnly);
                financialReportConsolidateDaily.AddFinancialSummary(entity);
                await _repository.Create(financialReportConsolidateDaily);
                return;
            }
            else 
            {
                financialReportConsolidateDaily.AddFinancialSummary(entity);
                await _repository.Update(financialReportConsolidateDaily.Id.ToString(),financialReportConsolidateDaily);
                return;
            }
        }

        private static string GetCorrelationId(Msg message)
        {
            try
            {
                return message.Header["correlationId"];
            }
            catch
            {
                return string.Empty;
            }

        }

      
    }
}
