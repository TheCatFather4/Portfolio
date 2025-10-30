using Cafe.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class AddItemForm : IValidatableObject
    {
        public SelectList? Categories { get; set; }

        [Required(ErrorMessage = "A category for the item is required.")]
        public int? SelectedCategoryID { get; set; }

        [Required(ErrorMessage = "A name for the item is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A description of the item is required.")]
        public string? Description { get; set; }

        public SelectList? TimeOfDays { get; set; }

        [Required(ErrorMessage = "A time of day is required.")]
        public int? SelectedTimeOfDayID { get; set; }

        [Required(ErrorMessage = "A price for the item is required.")]
        [Range(1.00, 20.00, ErrorMessage = "Item price must be between 1.00 and 20.00.")]
        public decimal? Price { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A start date is required")]
        public DateTime? Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime? End { get; set; }

        public Item ToEntity()
        {
            var item = new Item();

            item.CategoryID = SelectedCategoryID;
            item.ItemName = Name;
            item.ItemDescription = Description;
            item.ItemStatusID = 1;
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

            if (Start.HasValue && End.HasValue && Start.Value > End.Value)
            {
                errors.Add(new ValidationResult("The Start Date cannot be later than the End Date.", [nameof(Start), nameof(End)]));
            }

            return errors;
        }
    }
}