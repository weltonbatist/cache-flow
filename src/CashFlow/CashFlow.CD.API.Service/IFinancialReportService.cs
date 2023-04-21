using CashFlow.CD.Domain.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.CD.API.Service
{
    public interface IFinancialReportService
    {
        Task<FinancialReportConsolidateDaily> GetReport(DateOnly dateOnly);
    }
}
