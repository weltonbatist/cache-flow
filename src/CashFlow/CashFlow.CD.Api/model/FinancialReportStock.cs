using CashFlow.CD.Domain.domain;
using System.Collections.Generic;

namespace CashFlow.CD.Api.model
{
    public class FinancialReportStock
    {
        public FinancialReportStock(DateOnly date, decimal totalAmount, List<FinancialReportStoreStock> financialReportStoreStock)
        {
            Date = date.ToString("dd/MM/yyyy");
            TotalAmount = totalAmount;
            FinancialReportStoreStock = financialReportStoreStock;
        }

        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<FinancialReportStoreStock> FinancialReportStoreStock { get; set; }
        public static FinancialReportStock Convert(FinancialReportConsolidateDaily financialReportConsolidateDaily) 
        {
            if(financialReportConsolidateDaily is null)
                return null;

            decimal totalAmount = 0;
            var group = financialReportConsolidateDaily.FinancialSummaries.GroupBy(x => x.StoreId);
            List<FinancialReportStoreStock> financialReportStoreStocks = new List<FinancialReportStoreStock>();
            foreach (var item in group) 
            {
                FinancialReportStoreStock financialReportStoretock = new()
                {
                    Amount = item.Sum(x => x.Amount),
                    StoreId = item.Key
                };
                totalAmount += financialReportStoretock.Amount;
                financialReportStoreStocks.Add(financialReportStoretock);
            }

            return new FinancialReportStock(financialReportConsolidateDaily.Date, totalAmount, financialReportStoreStocks);
            
        }
    }
}
