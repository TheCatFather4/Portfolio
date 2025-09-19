namespace Portfolio.Models.Ordering
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public byte? PaymentStatusId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Tip { get; set; }
        public decimal FinalTotal { get; set; }
        public List<OrderItemDetails> Items { get; set; }
    }
}
