using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Management
{
    /// <summary>
    /// Used to add new servers or edit current servers in the database. Implements IValidatableObject for custom validation.
    /// </summary>
    public class ServerForm : IValidatableObject
    {
        /// <summary>
        /// The primary key/ID of the Server to add or edit.
        /// </summary>
        public int? ServerID { get; set; }

        /// <summary>
        /// The first name of the Server to add or edit.
        /// </summary>
        [Required(ErrorMessage = "A First Name is required.")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name of the Server to add or edit.
        /// </summary>
        [Required(ErrorMessage = "A Last Name is required.")]
        public string? LastName { get; set; }

        /// <summary>
        /// The hire date of the Server to add or edit.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        /// <summary>
        /// The temination date of the Server to add or edit. If blank, the server is considered active.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? TermDate { get; set; }

        /// <summary>
        /// The date of birth of the Server to add or edit.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; } = DateTime.Now;

        /// <summary>
        /// Constructs a new ServerForm object.
        /// </summary>
        public ServerForm()
        {

        }

        /// <summary>
        /// Constructs a new ServerForm object with data populated from a Server entity.
        /// </summary>
        /// <param name="entity">A Server entity with data.</param>
        public ServerForm(Server entity)
        {
            ServerID = entity.ServerID;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            HireDate = entity.HireDate;
            TermDate = entity.TermDate;
            DoB = entity.DoB;
        }

        /// <summary>
        /// Used to map the model to a new Server object.
        /// </summary>
        /// <returns>A new Server object.</returns>
        public Server ToEntity()
        {
            return new Server()
            {
                ServerID = ServerID ?? 0,
                FirstName = FirstName,
                LastName = LastName,
                HireDate = HireDate,
                TermDate = TermDate,
                DoB = DoB
            };
        }

        /// <summary>
        /// Checks to see if a server is atleast 18 years of age and if their hire date comes before their termination date.
        /// The date check only occurs if the termination date property has a value.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A list of errors.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (DoB.Date > DateTime.Today.AddYears(-18))
            {
                errors.Add(new ValidationResult("Servers must be atleast 18 years of age", ["DoB"]));
            }

            if (TermDate.HasValue && TermDate.Value < HireDate)
            {
                errors.Add(new ValidationResult("Termination Date must come after Hire Date", ["TermDate"]));
            }

            return errors;
        }
    }
}