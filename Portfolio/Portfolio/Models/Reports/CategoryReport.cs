namespace Portfolio.Models.Reports
{
    public class CategoryReport
    {
        public string ItemName { get; set; }
        public List<ItemReport> ItemDateReports { get; set; }
    }
}