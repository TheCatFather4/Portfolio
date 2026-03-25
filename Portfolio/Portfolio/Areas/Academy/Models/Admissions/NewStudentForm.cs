using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Areas.Academy.Models.Admissions
{
    /// <summary>
    /// A model used to register new students with the academy.
    /// </summary>
    public class NewStudentForm : IValidatableObject
    {
        /// <summary>
        /// The student's first name.
        /// </summary>
        [Required]
        public string? FirstName { get; set; }

        /// <summary>
        /// The student's last name.
        /// </summary>
        [Required]
        public string? LastName { get; set; }

        /// <summary>
        /// The student's nickname or superhero name.
        /// </summary>
        [Required]
        public string? Alias { get; set; }

        /// <summary>
        /// The student's birthday.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; } = DateTime.Now;

        public SelectList? Powers { get; set; }

        /// <summary>
        /// The selected power ID from the select list of powers.
        /// </summary>
        [Required]
        public int SelectedPowerID { get; set; }

        /// <summary>
        /// The level of strength of the student's power.
        /// </summary>
        public byte Rating { get; set; }

        public SelectList? Weaknesses { get; set; }

        /// <summary>
        /// The selected weakness ID from the select list of weaknesses.
        /// </summary>
        [Required]
        public int SelectedWeaknessID { get; set; }

        /// <summary>
        /// The level of intensity of the student's weakness.
        /// </summary>
        public byte RiskLevel { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (DoB.Date > DateTime.Today.AddYears(-18))
            {
                errors.Add(new ValidationResult("Contacts must be over 18!", ["DoB"]));
            }

            return errors;
        }
    }
}