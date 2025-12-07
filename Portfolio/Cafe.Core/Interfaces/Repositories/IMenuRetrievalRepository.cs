using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IMenuRetrievalRepository
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<List<Category>> GetCategoriesAsync();
        Task<Item?> GetItemByIdAsync(int itemId);
        Task<ItemPrice?> GetItemPriceByItemIdAsync(int itemId);
        Task<List<Item>> GetItemsByCategoryIdAsync(int categoryId);
        Task<List<TimeOfDay>> GetTimeOfDaysAsync();
    }
}