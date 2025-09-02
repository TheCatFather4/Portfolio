using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Reports
{
    public class OrderRevenue
    {
        public DateTime? OrderDate { get; set; }
        public List<CafeOrder>? Orders { get; set; }
        public decimal? TotalRevenue { get; set; }
    }
}
