using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.Attributes
{
    /// <summary>
    /// Specifies a decimal range of values for data validation.
    /// Inherits from ValidationAttribute.
    /// </summary>
    public class DecimalRange : ValidationAttribute
    {
        private readonly decimal _min;
        private readonly decimal _max;

        /// <summary>
        /// Constructs the object required to validate a range of decimal values.
        /// </summary>
        /// <param name="min">The minimum decimal.</param>
        /// <param name="max">The maximum decimal.</param>
        /// <exception cref="ArgumentException">An exception will be thrown if the strings are not parsable.</exception>
        public DecimalRange(string min, string max)
        {
            if (!decimal.TryParse(min, out _min) || !decimal.TryParse(max, out _max))
            {
                throw new ArgumentException("Invalid decimal range values provided.");
            }
        }

        /// <summary>
        /// An override for the IsValid method. Checks if range is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>Success or an error message depending on the input.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is decimal currentValue)
            {
                if (currentValue >= _min && currentValue <= _max)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult($"The field {validationContext.DisplayName} must be between {_min} and {_max}.");
        }
    }
}