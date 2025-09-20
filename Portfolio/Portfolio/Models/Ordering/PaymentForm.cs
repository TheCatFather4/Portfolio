using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Ordering
{
    public class PaymentForm
    {
        public int OrderId { get; set; }
        public SelectList? PaymentTypes { get; set; }

        [Required(ErrorMessage = "You must select a payment method.")]
        public byte? PaymentTypeId { get; set; }

        [Required(ErrorMessage = "You must enter a payment amount.")]
        public decimal? Amount { get; set; }

        public DateTime TransactionDate { get; set; }
        public byte PaymentStatusId { get; set; }
    }
}
