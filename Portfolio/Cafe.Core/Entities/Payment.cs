using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Core.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }

        [ForeignKey("CafeOrder")]
        public int OrderID { get; set; }

        public int PaymentTypeID { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public byte PaymentStatusID { get; set; }

        // Navigation properties
        public CafeOrder? Order { get; set; }
        public PaymentType? PaymentType { get; set; }
    }
}
