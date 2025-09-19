namespace Portfolio.Models.Ordering
{
    public class OrderForm
    {
        public int CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Tip { get; set; }
        public decimal FinalTotal { get; set; }
        public byte? PaymentStatusId { get; set; }
        public int OrderId { get; set; }
    }
}
