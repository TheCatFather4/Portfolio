using Cafe.Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Ordering
{
    public class OrderForm
    {
        public int CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }

        [Required(ErrorMessage = "This field is required. Enter 0 for no tip.")]
        [DecimalRange("0.00", "1000.00", ErrorMessage = "Tip amount must be between 0.00 and 1000.00.")]
        public decimal? Tip { get; set; }

        public decimal FinalTotal { get; set; }
        public byte? PaymentStatusId { get; set; }
        public int OrderId { get; set; }
        public decimal TipFormTotal { get; set; }
        public decimal TipTen { get; set; }
        public decimal TipFifteen { get; set; }
        public decimal TipTwenty { get; set; }
    }
}