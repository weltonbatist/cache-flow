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
        public string Id { get; set; }
        public DateOnly Date { get; private set; }
        public ICollection<FinancialSummary> FinancialSummaries { get; set; }

        public void AddFinancialSummary(FinancialSummary financialSummary)
        {
            if (FinancialSummaries == null)
                FinancialSummaries = new List<FinancialSummary>();
            FinancialSummaries.Add(financialSummary);
        }   
    }
}
