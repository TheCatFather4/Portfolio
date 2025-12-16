using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Core.Entities
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }

        [ForeignKey("CafeOrder")]
        public int OrderID { get; set; }
        public int ItemPriceID { get; set; }
        public byte Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }

        // Navigation property for joining CafeOrder table.
        public CafeOrder? CafeOrder { get; set; }
    }
}