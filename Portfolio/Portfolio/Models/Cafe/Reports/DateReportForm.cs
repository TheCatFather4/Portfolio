using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Cafe.Reports
{
    /// <summary>
    /// Used to display sales reports filtered by date.
    /// </summary>
    public class DateReportForm
    {
        /// <summary>
        /// The date chosen to display sales data.
        /// </summary>
        [Required(ErrorMessage = "A date is required to view reports.")]
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// A list of paid orders for the date selected.
        /// </summary>
        public List<CafeOrder>? Orders { get; set; }

        /// <summary>
        /// The total revenue of item's sold for the date selected.
        /// </summary>
        public decimal? TotalRevenue { get; set; }
    }
}