using Cafe.Core.Entities;

namespace Portfolio.Models
{
    public class MenuItem
    {
        public int? ItemID { get; set; }
        public int? CategoryID { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public List<ItemPrice>? Prices { get; set; }

        public MenuItem()
        {

        }

        public MenuItem(Item entity)
        {
            ItemID = entity.ItemID;
            CategoryID = entity.CategoryID;
            ItemName = entity.ItemName;
            ItemDescription = entity.ItemDescription;
            Prices = entity.Prices;
        }
    }
}
