using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Core.Entities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ItemID { get; set; }
        public int? CategoryID { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public byte? ItemStatusID { get; set; }
        public string? ItemImgPath { get; set; }

        public List<ItemPrice>? Prices { get; set; }
    }
}
