using Cafe.Core.Entities;

namespace Portfolio.Models
{
    public class ItemForm
    {
        public int? ItemID { get; set; }
        public int? CategoryID { get; set; }
        public string? ItemName { get; set; }
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
    }
}
