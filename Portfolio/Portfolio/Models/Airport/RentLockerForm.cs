namespace Portfolio.Models.Airport
{
    /// <summary>
    /// A model used to select a locker number to rent.
    /// </summary>
    public class RentLockerForm
    {
        /// <summary>
        /// The selected locker number.
        /// </summary>
        public string? LockerNumber { get; set; }

        /// <summary>
        /// The result message.
        /// </summary>
        public string? Message { get; set; }
    }
}