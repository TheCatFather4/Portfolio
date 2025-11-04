namespace Cafe.Core.DTOs
{
    public class CategoryFilter
    {
        public string ItemName { get; set; }
        public List<ItemFilter> ItemReports { get; set; }
    }
}