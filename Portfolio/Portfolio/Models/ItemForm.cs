using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class ItemForm : IValidatableObject
    {
        public int? ItemID { get; set; }
        public int? CategoryID { get; set; }

        [Required(ErrorMessage = "An item name is required.")]
        public string? ItemName { get; set; }

        [Required(ErrorMessage = "A description for the item is required.")]
        public string? ItemDescription { get; set; }

        public List<ItemPrice>? Prices { get; set; }

        public ItemForm()
        {

        }

        public ItemForm(Item entity)
        {
            ItemID = entity.ItemID;
            CategoryID = entity.CategoryID;
            ItemName = entity.ItemName;
            ItemDescription = entity.ItemDescription;
            Prices = entity.Prices;
        }

        public Item ToEntity()
        {
            return new Item()
            {
                ItemID = ItemID ?? 0,
                CategoryID = CategoryID,
                ItemName = ItemName,
                ItemDescription = ItemDescription,
                Prices = Prices
            };
        }

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
