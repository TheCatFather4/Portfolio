namespace Portfolio.Utilities
{
    /// <summary>
    /// A utility used to display alerts to the user. The returned strings make use of Bootstrap color attributes.
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// The message to display in the window.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The constructor for the alert. Takes a string parameter.
        /// </summary>
        /// <param name="message">The message you would like to communicate.</param>
        public Alert(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Returns the message and the Bootstrap "success" attribute.
        /// </summary>
        /// <param name="message">The message you would like to communicate.</param>
        /// <returns>An interpolated string with the message.</returns>
        public static string CreateSuccess(string message)
        {
            return $"{message}|success";
        }

        /// <summary>
        /// Returns the message and the Bootstrap "danger" attribute.
        /// </summary>
        /// <param name="message">The message you would like to communicate.</param>
        /// <returns>An interpolated string with the message.</returns>
        public static string CreateError(string message)
        {
            return $"{message}|danger";
        }

        /// <summary>
        /// Returns the message and the Bootstrap "info" attribute.
        /// </summary>
        /// <param name="message">The message you would like to communicate.</param>
        /// <returns>An interpolated string with the message.</returns>
        public static string CreateInfo(string message)
        {
            return $"{message}|info";
        }
    }
}