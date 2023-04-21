using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.CD.Domain.domain
{
    public class FinancialSummary
    {
        public FinancialSummary(decimal amount, DateTime date, int storeId, int cashRegisterId, string correlationId)
        {
            Amount = amount;
            Date = date;
            StoreId = storeId;
            CashRegisterId = cashRegisterId;
            CorrelationId = correlationId;
        }

        public FinancialSummary()
        {
            
        }

        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
        public int CashRegisterId { get; set; }
        public string CorrelationId { get; set; }
    }
}
