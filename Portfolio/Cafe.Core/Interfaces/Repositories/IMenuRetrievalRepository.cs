using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IMenuRetrievalRepository
    {
        List<Item> GetAllItems();
        List<Category> GetCategories();
        Task<Item> GetItemByIdAsync(int itemId);
        Task<ItemPrice> GetItemPriceByItemIdAsync(int itemId);
        List<Item> GetItemsByCategoryId(int categoryId);
        List<TimeOfDay> GetTimeOfDays();
    }
}