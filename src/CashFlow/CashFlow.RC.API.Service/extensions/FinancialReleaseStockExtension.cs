using CashFlow.RC.Common.dto;
using CashFlow.RC.Domain.domain;
using CashFlow.RC.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.API.Service.extensions
{
    public static class FinancialReleaseStockExtension
    {
        public static FinancialRelease ConvertToEntity(this FinancialReleaseStock financialReleaseStock)
        {
            return new FinancialRelease
            {
                Amount = financialReleaseStock.Amount,
                CashRegisterId = financialReleaseStock.CashRegisterId,
                Date = financialReleaseStock.Date,
                ReleaseType = (ReleaseType)financialReleaseStock.ReleaseType,
                StoreId = financialReleaseStock.StoreId,
            };
        }
    }
}
