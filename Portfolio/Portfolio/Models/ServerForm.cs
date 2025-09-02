using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class ServerForm : IValidatableObject
    {
        public int? ServerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TermDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DoB { get; set; } = DateTime.Now;

        public ServerForm()
        {

        }

        public ServerForm(Server entity)
        {
            ServerID = entity.ServerID;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            HireDate = entity.HireDate;
            TermDate = entity.TermDate;
            DoB = entity.DoB;
        }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (DoB.Date > DateTime.Today.AddYears(-18))
            {
                errors.Add(new ValidationResult("Servers must be over 18 years of age", ["DoB"]));
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                errors.Add(new ValidationResult("A first name is required"));
            }

            if (string.IsNullOrEmpty(LastName))
            {
                errors.Add(new ValidationResult("A last name is required"));
            }

            return errors;
        }
    }
}
