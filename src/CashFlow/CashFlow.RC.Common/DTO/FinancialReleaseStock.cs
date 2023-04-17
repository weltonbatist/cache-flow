using CashFlow.RC.Common.extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.Common.dto
{
    public class FinancialReleaseStock
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be null or empty!")]
        public decimal Amount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be null or empty!")]
        public int ReleaseType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be null or empty!")]
        public DateTime Date { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be null or empty!")]
        public int StoreId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be null or empty!")]
        public int CashRegisterId { get; set; }

        public Guid GetKey()
        {
            return $"{StoreId}{CashRegisterId}{Date}{Amount}{ReleaseType}".ConvertToGuid();
        }

        public bool IsValid()
        {
            return StoreId > 0 && CashRegisterId > 0 && Date > DateTime.MinValue && Date < DateTime.MaxValue && Amount != 0;
        }
    }
}
