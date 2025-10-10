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
        Task<Result<Item>> GetItem(int id);
        Task<Result<ItemPrice>> GetItemPriceByIdAsync(int itemId);
    }
}