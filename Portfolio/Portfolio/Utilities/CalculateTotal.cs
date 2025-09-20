using Cafe.Core.Entities;

namespace Portfolio.Utilities
{
    public static class CalculateTotal
    {
        public static decimal AddItems(List<ShoppingBagItem> items)
        {
            decimal total = 0;

            foreach (var item in items)
            {
                var price = item.Price;
                var quantity = item.Quantity;

                var itemTotal = price * quantity;

                total += (decimal)itemTotal;
            }

            return total;
        }
    }
}
