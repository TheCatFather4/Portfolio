using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuRetrievalService
    {
        Task<Result<List<ItemResponse>>> GetAllItemsAPIAsync();
        Task<Result<List<Item>>> GetAllItemsMVCAsync();
        Result<List<Category>> GetCategories();
        Task<Result<ItemResponse>> GetItemByIdAsyncAPI(int itemId);
        Task<Result<Item>> GetItemByIdAsyncMVC(int itemID);
        Result<List<Item>> GetItemsByCategoryId(int categoryId);
        Result<List<TimeOfDay>> GetTimeOfDays();
    }
}