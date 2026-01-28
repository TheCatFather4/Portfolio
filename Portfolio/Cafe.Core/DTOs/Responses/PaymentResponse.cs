namespace Cafe.Core.DTOs.Responses
{
    public class PaymentResponse
    {
        /// <summary>
        /// Used for displaying payment confirmation data to the client.
        /// </summary>
        public int OrderID { get; set; }
        public int PaymentStatusID { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}