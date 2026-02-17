using Cafe.Core.Enums;

namespace Portfolio.Utilities
{
    /// <summary>
    /// Used to convert Enums for a customer's order history in the 4th Wall Café application.
    /// </summary>
    public class EnumConverter
    {
        /// <summary>
        /// Retrieves an item's name from the ItemPriceName enum, by ItemPriceID.
        /// </summary>
        /// <param name="itemPriceId">The ID of the item price.</param>
        /// <returns>The name of the item.</returns>
        public static string GetItemPriceName(int itemPriceId)
        {
            foreach (ItemPriceName ipn in Enum.GetValues<ItemPriceName>())
            {
                if (itemPriceId == (int)ipn)
                {
                    if (ipn.ToString().Contains("_"))
                    {
                        return ipn.ToString().Replace("_", " ");
                    }

                    return ipn.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Retrieves a payment type name from the PaymentTypeName enum, by PaymentTypeID.
        /// </summary>
        /// <param name="paymentTypeId">The ID of the payment type.</param>
        /// <returns>The name of the payment type.</returns>
        public static string GetPaymentTypeName(int paymentTypeId)
        {
            foreach (PaymentTypeName ptn in Enum.GetValues<PaymentTypeName>())
            {
                if (paymentTypeId == (int)ptn)
                {
                    if (ptn.ToString().Contains("_"))
                    {
                        return ptn.ToString().Replace("_", " ");
                    }

                    return ptn.ToString();
                }
            }

            return string.Empty;
        }
    }
}