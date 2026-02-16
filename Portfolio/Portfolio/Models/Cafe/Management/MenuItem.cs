using Cafe.Core.Entities;

namespace Portfolio.Models.Cafe.Management
{
    /// <summary>
    /// A model used to display menu items for management.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// The item's ID.
        /// </summary>
        public int? ItemID { get; set; }

        /// <summary>
        /// The item's category ID.
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string? ItemName { get; set; }

        /// <summary>
        /// A description of the item.
        /// </summary>
        public string? ItemDescription { get; set; }

        /// <summary>
        /// A list of prices for the item.
        /// </summary>
        public List<ItemPrice>? Prices { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MenuItem()
        {

        }

        /// <summary>
        /// Constructor overload that maps an item entity to this model.
        /// </summary>
        /// <param name="entity">An item entity.</param>
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