﻿
using CashFlow.RC.Domain.enums;
using CashFlow.Util.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.Domain.domain
{
    public class FinancialRelease
    {
        
        public Guid Key => $"{StoreId}{CashRegisterId}{Date}{Amount}{ReleaseType}".ConvertToGuid();
        public decimal Amount { get; set; }
        public ReleaseType ReleaseType { get; set; }
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
        public int CashRegisterId { get; set; }

        public bool IsValid() 
        {
         return Amount != 0 && Date != DateTime.MinValue && StoreId > 0 && CashRegisterId > 0;
        }
    }
}
