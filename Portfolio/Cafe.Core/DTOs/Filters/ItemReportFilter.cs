namespace Cafe.Core.DTOs.Filters
{
    /// <summary>
    /// Used to filter an item for a sales report.
    /// </summary>
    public class ItemReportFilter
    {
        public decimal Price { get; set; }
        public byte Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
        public DateTime DateSold { get; set; }
    }
}