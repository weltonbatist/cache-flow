using CashFlow.CD.Domain.domain;

namespace CashFlow.CD.Domain.Test
{
    public class FinancialReportConsolidateDailyTest
    {
        [Fact]
        public void CreatingAValidEntity()
        {
            FinancialReportConsolidateDaily financialReportConsolidateDaily = new FinancialReportConsolidateDaily(new DateOnly(2023, 4, 30));
            financialReportConsolidateDaily.Id = Guid.NewGuid().ToString();
            financialReportConsolidateDaily.AddFinancialSummary(new FinancialSummary(100, DateTime.Now, 1, 1, Guid.NewGuid().ToString()));
            financialReportConsolidateDaily.AddFinancialSummary(new FinancialSummary(200, DateTime.Now, 1, 1, Guid.NewGuid().ToString()));
            financialReportConsolidateDaily.AddFinancialSummary(new FinancialSummary(300, DateTime.Now, 1, 1, Guid.NewGuid().ToString()));
            Assert.True(financialReportConsolidateDaily.FinancialSummaries.Count() == 3);
        }
        
    }
}