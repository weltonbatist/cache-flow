using CashFlow.RC.Domain.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.Domain.repository
{
    public interface IFinancialReleaseRepository
    {
        Task Add(FinancialRelease financialRelease);
        Task Delete(FinancialRelease financialRelease);
        Task<FinancialRelease> Get(Guid key);
        Task<IEnumerable<FinancialRelease>> GetAll();
    }
}
