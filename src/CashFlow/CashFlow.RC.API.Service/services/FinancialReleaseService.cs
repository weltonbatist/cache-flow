using CashFlow.NATS;
using CashFlow.RC.API.Service.extensions;
using CashFlow.RC.Common.dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CashFlow.RC.API.Service.services
{
    public class FinancialReleaseService
    {
        private const string TOPIC = "cashflow.financialrelease";
        private readonly ILogger<FinancialReleaseService> _logger;
        private readonly NatsPublisher _publisher;
        public FinancialReleaseService(ILogger<FinancialReleaseService> logger, NatsPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public Task<bool> LaunchNewCashFlow(FinancialReleaseStock financialReleaseStock, string correlationId) 
        {
            var entity = financialReleaseStock.ConvertToEntity();

            try
            {
                _publisher.Publish(TOPIC, JsonSerializer.Serialize(entity), correlationId);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{correlationId}: Error on publish new cash flow");
                return Task.FromResult(false);
            }
        }
    }
}
