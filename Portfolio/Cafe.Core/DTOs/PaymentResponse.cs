namespace Cafe.Core.DTOs
{
    public class PaymentResponse
    {
        public int OrderID { get; set; }
        public int PaymentStatusID { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}