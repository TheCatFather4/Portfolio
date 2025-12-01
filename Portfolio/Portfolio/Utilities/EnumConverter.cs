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
    }
}