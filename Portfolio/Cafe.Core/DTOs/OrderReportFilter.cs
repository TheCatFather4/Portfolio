using Cafe.Core.Entities;

namespace Cafe.Core.DTOs
{
    public class OrderReportFilter
    {
        public List<CafeOrder> Orders { get; set; }
        public decimal Revenue { get; set; }
    }
}