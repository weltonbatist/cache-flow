using CashFlow.Util.extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.CD.Domain.domain
{
    public class FinancialReportConsolidateDaily
    {

        public FinancialReportConsolidateDaily(DateOnly date)
        {
            Date = date;
            Id = Guid.NewGuid().ToString();
        }

        public FinancialReportConsolidateDaily()
        {
            
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public virtual string Id { get; set; }
        public virtual DateOnly Date { get; private set; }
        public virtual ICollection<FinancialSummary> FinancialSummaries { get; set; }

        public virtual void AddFinancialSummary(FinancialSummary financialSummary)
        {
            if (FinancialSummaries == null)
                FinancialSummaries = new List<FinancialSummary>();
            FinancialSummaries.Add(financialSummary);
        }   
    }
}
