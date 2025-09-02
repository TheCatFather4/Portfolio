using Cafe.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class AddItem : IValidatableObject
    {
        public SelectList? Categories { get; set; }
        public int? SelectedCategoryID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public SelectList? TimeOfDays { get; set; }
        public int? SelectedTimeOfDayID { get; set; }
        public decimal? Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime? End { get; set; }

        public Item ToEntity()
        {
            var item = new Item();

            item.CategoryID = SelectedCategoryID;
            item.ItemName = Name;
            item.ItemDescription = Description;
            item.Prices = new List<ItemPrice>();

            var itemPrice = new ItemPrice();

            itemPrice.TimeOfDayID = SelectedTimeOfDayID;
            itemPrice.Price = Price;
            itemPrice.StartDate = Start;
            itemPrice.EndDate = End;

            item.Prices.Add(itemPrice);

            return item;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add(new ValidationResult("An item name is required."));
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                errors.Add(new ValidationResult("An item description is required."));
            }

            return errors;
        }
    }
}
