using CashFlow.CD.Domain.domain;
using CashFlow.NATS;
using CashFlow.RC.Domain.domain;
using CashFlow.RC.Domain.repository;
using NATS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CashFlow.RC.Work.service
{
    public class PersistenceService
    {
        private readonly ILogger<PersistenceService> _logger;
        private readonly IFinancialReleaseRepository _repository;
        private readonly NatsPublisher _publisher;
        private const string TOPIC = "cashflow.summary";
        public PersistenceService(ILogger<PersistenceService> logger, IFinancialReleaseRepository repository, NatsPublisher publisher)
        {
            _logger = logger;
            _repository = repository;
            _publisher = publisher;
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

            var entity = JsonSerializer.Deserialize<FinancialRelease>(data);

            if(entity is null) 
            {
                _logger.LogWarning($"Message received with no entity(FinancialRelease). CorrelationId: {correlationId}");
                return;
            }

            await _repository.Create(entity);

            _logger.LogInformation($"Registered entity. CorrelationId: {correlationId}");


            await SendToSummaryService(entity, correlationId);
        }

        private Task SendToSummaryService(FinancialRelease financialRelease, string correlationId)
        {
            FinancialSummary financialSummary = new(
                financialRelease.Amount, 
                financialRelease.Date, 
                financialRelease.StoreId, 
                financialRelease.CashRegisterId, 
                correlationId);

            _publisher.Publish(TOPIC, JsonSerializer.Serialize(financialSummary), correlationId);
            return Task.CompletedTask;
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
