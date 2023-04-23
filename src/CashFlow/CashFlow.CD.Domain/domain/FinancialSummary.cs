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

        public virtual long Id { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int StoreId { get; set; }
        public virtual int CashRegisterId { get; set; }
        public virtual string CorrelationId { get; set; }
    }
}
