using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.Common.DTO
{
    public class FinancialReleaseStock
    {
        public decimal Amount { get; set; }
        public int ReleaseType { get; set; }
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
        public int CashRegisterId { get; set; }
    }
}
