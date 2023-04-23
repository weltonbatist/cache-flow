using CashFlow.CD.Api.model;
using CashFlow.CD.API.Service;
using CashFlow.CD.Domain.domain;
using Moq;

namespace CashFlow.CD
{
    public class FinancialReportServiceTest
    {
       
        [Fact]
        public void GettingTheCashFlow()
        {
            Mock<IFinancialReportService> _financialReportService = new Mock<IFinancialReportService>();
            var date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var entity = GetFinancialReportConsolidateDaily(date);
            _financialReportService.Setup(x => 
            x.GetReport(new DateOnly(
                DateTime.Now.Year, 
                DateTime.Now.Month, 
                DateTime.Now.Day)))
                    .Returns(() => Task.FromResult(entity));

            
            var entityDb = _financialReportService.Object.GetReport(date).Result;

            var report = FinancialReportStock.Convert(entityDb);

            Assert.NotNull(report);
            Assert.True(report.FinancialReportStoreStock.Count() == 2);
            Assert.True(report.TotalAmount == 1000);
            Assert.True(report.TotalAmount == report.FinancialReportStoreStock.Sum(x => x.Amount));
        }

        [Fact]
        public void GettingTheCashFlowNotFound()
        {
            Mock<IFinancialReportService> _financialReportService = new Mock<IFinancialReportService>();
            var date = new DateOnly(2022, 1, 1);
            var entity = GetFinancialReportConsolidateDaily(date);
            _financialReportService.Setup(x =>
            x.GetReport(new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day)))
                    .Returns(() => Task.FromResult(entity));

            var entityDb = _financialReportService.Object.GetReport(date).Result;

            var report = FinancialReportStock.Convert(entityDb);

            Assert.Null(report);
           
        }

        private FinancialReportConsolidateDaily GetFinancialReportConsolidateDaily(DateOnly dateOnly) 
        {
            FinancialReportConsolidateDaily financialReportConsolidateDaily = new FinancialReportConsolidateDaily(dateOnly);
            financialReportConsolidateDaily.AddFinancialSummary(new FinancialSummary(100, DateTime.Now, 1, 1,Guid.NewGuid().ToString()));
            financialReportConsolidateDaily.AddFinancialSummary(new FinancialSummary(200, DateTime.Now, 1, 1, Guid.NewGuid().ToString()));
            financialReportConsolidateDaily.AddFinancialSummary(new FinancialSummary(300, DateTime.Now, 2, 1, Guid.NewGuid().ToString()));
            financialReportConsolidateDaily.AddFinancialSummary(new FinancialSummary(400, DateTime.Now, 2, 1, Guid.NewGuid().ToString()));
            return financialReportConsolidateDaily;
        }
    }
}