using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models.Ordering
{
    public class PaymentForm
    {
        public int OrderId { get; set; }
        public SelectList? PaymentTypes { get; set; }
        public byte PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public byte PaymentStatusId { get; set; }
    }
}
