using Cafe.Core.DTOs.Responses;

namespace Portfolio.Utilities
{
    /// <summary>
    /// Used for calculating prices in the 4th Wall Café application.
    /// </summary>
    public static class CalculateTotal
    {
        /// <summary>
        /// Takes a list of shopping bag items and calculates the total.
        /// </summary>
        /// <param name="items">A list of shopping bag items.</param>
        /// <returns>The total price of all items in decimal form.</returns>
        public static decimal AddItems(List<ShoppingBagItemResponse> items)
        {
            decimal total = 0;

            foreach (var item in items)
            {
                var price = item.Price;
                var quantity = item.Quantity;

                var itemTotal = price * quantity;

                total += itemTotal;
            }

            return total;
        }
    }
}