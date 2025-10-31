using Cafe.Core.Entities;

namespace Cafe.Core.DTOs
{
    public class OrderDateFilter
    {
        public List<CafeOrder> Orders { get; set; }
        public decimal Revenue { get; set; }
    }
}