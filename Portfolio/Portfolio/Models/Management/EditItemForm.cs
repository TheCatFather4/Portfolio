using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Management
{
    /// <summary>
    /// Used to edit an item's data. Implements IValidatableObject for custom validation.
    /// </summary>
    public class EditItemForm : IValidatableObject
    {
        /// <summary>
        /// The primary key/ID of the item to edit.
        /// </summary>
        public int? ItemID { get; set; }

        /// <summary>
        /// The primary key/ID associated to the category the item belongs to.
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// The updated name of the item.
        /// </summary>
        [Required(ErrorMessage = "An item name is required.")]
        public string? ItemName { get; set; }

        /// <summary>
        /// The updated description of the item.
        /// </summary>
        [Required(ErrorMessage = "A description for the item is required.")]
        public string? ItemDescription { get; set; }

        /// <summary>
        /// The image path for the item. This is used to display custom images for each menu item.
        /// </summary>
        public string? ItemImgPath { get; set; }

        /// <summary>
        /// A list of ItemPrice objects associated with the item.
        /// </summary>
        public List<ItemPrice>? Prices { get; set; }

        /// <summary>
        /// Constructs an EditItemForm object.
        /// </summary>
        public EditItemForm()
        {

        }

        /// <summary>
        /// Constructs an EditItemForm object that is populated with an Item entity's data.
        /// </summary>
        /// <param name="entity">An Item entity with data.</param>
        public EditItemForm(Item entity)
        {
            ItemID = entity.ItemID;
            CategoryID = entity.CategoryID;
            ItemName = entity.ItemName;
            ItemDescription = entity.ItemDescription;
            ItemImgPath = entity.ItemImgPath;
            Prices = entity.Prices;
        }

        /// <summary>
        /// Used to map the model to a new Item entity.
        /// </summary>
        /// <returns>A new Item entity.</returns>
        public Item ToEntity()
        {
            return new Item()
            {
                ItemID = ItemID ?? 0,
                CategoryID = CategoryID,
                ItemName = ItemName,
                ItemDescription = ItemDescription,
                ItemImgPath = ItemImgPath,
                Prices = Prices
            };
        }

        /// <summary>
        /// Checks to see if an item's price is within a reasonable range.\
        /// Implements Validate from the interface.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A list of errors.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            for (int i = 0; i < Prices.Count; i++)
            {
                var itemPrice = Prices[i];

                if (itemPrice.Price.HasValue && (itemPrice.Price < 1 || itemPrice.Price > 20))
                {
                    string memberName = $"{nameof(Prices)}[{i}].{nameof(ItemPrice.Price)}";

                    errors.Add(new ValidationResult("Price must be atleast 1 dollar and less than 20 dollars.", new[] { memberName }));
                }
            }

            return errors;
        }
    }
}