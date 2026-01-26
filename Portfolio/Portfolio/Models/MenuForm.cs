using Cafe.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models
{
    /// <summary>
    /// Used for selecting and displaying menu items to the user.
    /// </summary>
    public class MenuForm
    {
        /// <summary>
        /// A select list that includes each category name and ID.
        /// </summary>
        public SelectList? Categories { get; set; }

        /// <summary>
        /// The category the user selects will be set here as its primary key/ID.
        /// </summary>
        public int? SelectedCategoryID { get; set; }

        /// <summary>
        /// A select list that includes each time of day name and ID.
        /// </summary>
        public SelectList? TimesOfDays { get; set; }

        /// <summary>
        /// The time of day the user selects will be set here as its primary key/ID.
        /// </summary>
        public int? SelectedTimeOfDayID { get; set; }

        /// <summary>
        /// The date that the user selects.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// The list of items that is retrieved after the user makes their selection.
        /// </summary>
        public IEnumerable<Item>? Items { get; set; }
    }
}