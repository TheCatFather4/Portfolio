using Cafe.Core.Entities;

namespace Cafe.Core.DTOs.Filters
{
    /// <summary>
    /// Used to filter and map sales data concerning a particular date.
    /// </summary>
    public class OrderDateFilter
    {
        public List<CafeOrder>? Orders { get; set; }
        public decimal Revenue { get; set; }
    }
}