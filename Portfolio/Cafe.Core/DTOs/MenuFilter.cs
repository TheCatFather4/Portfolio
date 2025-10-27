namespace Cafe.Core.DTOs
{
    public class MenuFilter
    {
        public int? CategoryID { get; set; }
        public int? TimeOfDayID { get; set; }
        public DateTime? Date { get; set; }
    }
}