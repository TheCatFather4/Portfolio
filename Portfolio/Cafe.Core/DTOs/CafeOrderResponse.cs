namespace Cafe.Core.DTOs
{
    public class CafeOrderResponse
    {
        public int OrderID { get; set; }
        public int? ServerID { get; set; }
        public int PaymentTypeID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Tip { get; set; }
        public decimal FinalTotal { get; set; }
        public int? CustomerID { get; set; }
        public int? PaymentStatusID { get; set; }

        public List<OrderItemResponse> OrderItems { get; set; } = new List<OrderItemResponse>();
    }
}
