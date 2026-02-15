using Airport.Core.DTOs;

namespace Portfolio.Models.Airport
{
    /// <summary>
    /// A model used to display an individual locker's contents.
    /// </summary>
    public class ViewLockerForm
    {
        /// <summary>
        /// The locker's number.
        /// </summary>
        public string? LockerNumber { get; set; }

        /// <summary>
        /// The result message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// The locker object selected. Includes renter name and locker contents.
        /// </summary>
        public Locker? Locker { get; set; }
    }
}