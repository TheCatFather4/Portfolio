using Cafe.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Cafe.Management
{
    /// <summary>
    /// Used to add new items to the menu. Implements IValidatableObject for custom validation.
    /// </summary>
    public class AddItemForm : IValidatableObject
    {
        /// <summary>
        /// A select list of categories. The user picks the category they want the new item to be associated with.
        /// </summary>
        public SelectList? Categories { get; set; }

        /// <summary>
        /// The primary key/ID of the category that the user picked.
        /// </summary>
        [Required(ErrorMessage = "A category for the item is required.")]
        public int? SelectedCategoryID { get; set; }

        /// <summary>
        /// The desired name of the new item.
        /// </summary>
        [Required(ErrorMessage = "A name for the item is required.")]
        public string? Name { get; set; }

        /// <summary>
        /// The desired description of the new item.
        /// </summary>
        [Required(ErrorMessage = "A description of the item is required.")]
        public string? Description { get; set; }

        /// <summary>
        /// A select list of time of days. The user picks the time of day that they want the new item to be available in.
        /// </summary>
        public SelectList? TimeOfDays { get; set; }

        /// <summary>
        /// The primary key/ID of the selected time of day.
        /// </summary>
        [Required(ErrorMessage = "A time of day is required.")]
        public int? SelectedTimeOfDayID { get; set; }

        /// <summary>
        /// The desired price of the new item.
        /// </summary>
        [Required(ErrorMessage = "A price for the item is required.")]
        [Range(1.00, 20.00, ErrorMessage = "Item price must be between 1.00 and 20.00.")]
        public decimal? Price { get; set; }

        /// <summary>
        /// The starting date that the item is available on the menu.
        /// </summary>
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A start date is required")]
        public DateTime? Start { get; set; }

        /// <summary>
        /// The ending date that the item is no longer available after.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? End { get; set; }

        /// <summary>
        /// Maps the model to an Item entity.
        /// </summary>
        /// <returns>A new Item entity with a list of item prices.</returns>
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

        /// <summary>
        /// Checks to see if the start date comes before the end date.
        /// Implements Validate from the interface.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A list of errors.</returns>
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