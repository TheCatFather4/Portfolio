using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.Attributes
{
    public class DecimalRange : ValidationAttribute
    {
        private readonly decimal _min;
        private readonly decimal _max;

        public DecimalRange(string min, string max)
        {
            if (!decimal.TryParse(min, out _min) || !decimal.TryParse(max, out _max))
            {
                throw new ArgumentException("Invalid decimal range values provided.");
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
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