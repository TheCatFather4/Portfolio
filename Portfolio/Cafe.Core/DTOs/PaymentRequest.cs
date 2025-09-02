namespace Cafe.Core.DTOs
{
    public class PaymentRequest
    {
        public int OrderID { get; set; }
        public int PaymentTypeID { get; set; }
        public decimal Amount { get; set; }
    }
}
