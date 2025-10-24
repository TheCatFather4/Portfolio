using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuService
    {
        Result<List<Category>> GetCategories();
        Result<List<TimeOfDay>> GetTimeOfDays();
        Task<Result<ItemPrice>> GetItemPriceByIdAsync(int itemId);
        Result<List<ItemResponse>> GetMenuAPI();
        Task<Result<ItemResponse>> GetItemAPIAsync(int itemId);
        Result<List<Item>> GetAllItems();
    }
}