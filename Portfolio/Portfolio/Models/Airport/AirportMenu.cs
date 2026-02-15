namespace Portfolio.Models.Airport
{
    /// <summary>
    /// A model used for user input and display.
    /// </summary>
    public class AirportMenu
    {
        /// <summary>
        /// The menu option that the user selects.
        /// </summary>
        public string? MenuChoice { get; set; }

        /// <summary>
        /// A message notifying the user of the result of their choice.
        /// </summary>
        public string? Message { get; set; }
    }
}