using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Ordering
{
    public class ItemUpdate
    {
        public int CustomerID { get; set; }
        public int ShoppingBagItemID { get; set; }

        [Required(ErrorMessage = "A quantity is required")]
        [Range(1, 9, ErrorMessage = "Quantity must be between 1 and 9.")]
        public byte? Quantity { get; set; }

        public string ItemName { get; set; }
        public string? ItemImgPath { get; set; }
        public decimal Price { get; set; }
    }
}