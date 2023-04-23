using CashFlow.RC.Domain.domain;
using CashFlow.RC.Domain.enums;

namespace CashFlow.RC.Domain.Test
{
    public class FinancialReleaseTest
    {
        [Fact]
        public void CreatingAValidEntity()
        {
            FinancialRelease financialRelease = new()
            {
                Amount = 100,
                ReleaseType = ReleaseType.creditor,
                Date = DateTime.Now,
                StoreId = 1,
                CashRegisterId = 1
            };

            Assert.True(financialRelease.IsValid());
        }
        [Fact]
        public void CreatingAnInvalidEntity()
        {
            FinancialRelease financialRelease = new()
            {
                Amount = 0,
                ReleaseType = ReleaseType.creditor,
                Date = DateTime.MinValue,
                StoreId = 0,
                CashRegisterId = 0
            };
            Assert.False(financialRelease.IsValid());
        }
    }
}