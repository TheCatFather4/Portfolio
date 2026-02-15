namespace Portfolio.Models.Airport
{
    /// <summary>
    /// A model used to retrieve renter details from the user.
    /// </summary>
    public class AddLockerForm
    {
        /// <summary>
        /// The locker number that the user is renting.
        /// </summary>
        public int LockerNumber { get; set; }

        /// <summary>
        /// The name of the renter.
        /// </summary>
        public string? RenterName { get; set; }

        /// <summary>
        /// The contents stored in the locker.
        /// </summary>
        public string? Contents { get; set; }

        /// <summary>
        /// A message used for validation.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// A message used for confirmation.
        /// </summary>
        public string? SuccessMessage { get; set; }
    }
}