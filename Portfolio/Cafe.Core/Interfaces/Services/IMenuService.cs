using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuService
    {
        Result<List<ItemResponse>> GetAllItemsAPI();
        Result<List<Item>> GetAllItemsMVC();
        Result<List<Category>> GetCategories();
        Task<Result<ItemResponse>> GetItemByIdAsyncAPI(int itemId);
        Task<Result<Item>> GetItemByIdAsyncMVC(int itemID);
        Task<Result<ItemPrice>> GetItemPriceByItemIdAsync(int itemId);
        Result<List<TimeOfDay>> GetTimeOfDays();
    }
}