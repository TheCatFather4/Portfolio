using Cafe.Core.Entities;

namespace Portfolio.Models.Reports
{
    public class ItemPriceReport
    {
        public Item? Item { get; set; }
        public ItemPrice? ItemPrice { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalExtendedPrice { get; set; }
    }
}
