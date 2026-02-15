using Airport.Core.DTOs;

namespace Portfolio.Models.Airport
{
    /// <summary>
    /// A model used to display all rented lockers and their contents.
    /// </summary>
    public class AllLockersDisplay
    {
        /// <summary>
        /// A list of all the lockers.
        /// </summary>
        public List<Locker>? Lockers { get; set; }

        /// <summary>
        /// A notification message that is displayed if no lockers are currently rented.
        /// </summary>
        public string? Message { get; set; }
    }
}