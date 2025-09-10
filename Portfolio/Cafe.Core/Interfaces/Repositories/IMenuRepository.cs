using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IMenuRepository
    {        
        List<Item> GetMenu();
        List<Category> GetCategories();
        List<TimeOfDay> GetTimeOfDays();
        List<Item> GetItems();
        Item GetItem(int id);
        List<Item> GetItemsByCategory(string categoryName);
        Task<Item> GetItemAsync(int itemId);
        Task<ItemPrice> GetItemPriceByIdAsync(int itemId);
        Task<Item> GetItemWithPriceAsync(int itemId);
    }
}
