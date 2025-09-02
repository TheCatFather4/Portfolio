using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuService
    {
        Result<List<Item>> GetMenu();
        Result<List<Category>> GetCategories();
        Result<List<TimeOfDay>> GetTimeOfDays();
        Result<List<Item>> GetItems();
        Result<Item> GetItem(int id);
        Result<List<Item>> GetItemsByCatetegory(string catetegoryName);
        Task<Result<ItemPrice>> GetItemPriceByIdAsync(int itemId);
    }
}
