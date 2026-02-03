namespace Cafe.Core.DTOs.Filters
{
    /// <summary>
    /// Used for filtering menu items.
    /// All properties or none may be used in menu filtration.
    /// </summary>
    public class MenuFilter
    {
        /// <summary>
        /// The selected Category ID.
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// The selected Time of Day ID.
        /// </summary>
        public int? TimeOfDayID { get; set; }

        /// <summary>
        /// The selected Date.
        /// </summary>
        public DateTime? Date { get; set; }
    }
}