using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.Entities
{
    public class CafeOrder
    {
        [Key]
        public int OrderID { get; set; }
        public int? ServerID { get; set; }
        public int PaymentTypeID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Tip { get; set; }
        public decimal FinalTotal { get; set; }
        public int? CustomerID { get; set; }
        public byte? PaymentStatusID { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
