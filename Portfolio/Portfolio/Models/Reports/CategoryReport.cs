namespace Portfolio.Models.Reports
{
    public class CategoryReport
    {
        public string ItemName { get; set; }
        public List<ItemDateReport> ItemDateReports { get; set; }
    }
}
