using CashFlow.CD.Domain.domain;
using CashFlow.CD.Domain.repository;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace CashFlow.CD.API.Service
{
    public class FinancialReportService: IFinancialReportService
    {
        private readonly ILogger<FinancialReportService> _logger;
        private IEntityRepository<FinancialReportConsolidateDaily> _repository;
        public FinancialReportService(
            ILogger<FinancialReportService> logger, 
            IEntityRepository<FinancialReportConsolidateDaily> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<FinancialReportConsolidateDaily> GetReport(DateOnly dateOnly) 
        {
            try
            {
                var filter = Builders<FinancialReportConsolidateDaily>.Filter.Eq("Date", dateOnly);
                return await _repository.FindOneAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetReport)}:Error => {ex.Message}");
                throw new Exception("Failed to process the report");
            }
        }
    }
}