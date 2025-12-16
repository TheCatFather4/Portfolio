using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Core.Entities
{
    public class ItemPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ItemPriceID { get; set; }
        public int? ItemID { get; set; }
        public int? TimeOfDayID { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation property for joining OrderItem table.
        public List<OrderItem>? OrderItems { get; set; }
    }
}