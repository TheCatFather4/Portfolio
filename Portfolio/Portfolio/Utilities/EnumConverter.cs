using Cafe.Core.Enums;

namespace Portfolio.Utilities
{
    public class EnumConverter
    {
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