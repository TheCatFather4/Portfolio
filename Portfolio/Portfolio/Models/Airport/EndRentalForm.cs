namespace Portfolio.Models.Airport
{
    /// <summary>
    /// A model used to end a locker rental.
    /// </summary>
    public class EndRentalForm
    {
        /// <summary>
        /// The selected locker number to end the rental.
        /// </summary>
        public string? LockerNumber { get; set; }

        /// <summary>
        /// A message used for validation.
        /// </summary>
        public string? Message { get; set; }
    }
}