using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        List<Item> GetMenu();
        List<Category> GetCategories();
        List<TimeOfDay> GetTimeOfDays();
        List<Item> GetItems();
        Task<ItemPrice> GetItemPriceByIdAsync(int itemId);
        Task<Item> GetItemWithPriceAsync(int itemId);
    }
}