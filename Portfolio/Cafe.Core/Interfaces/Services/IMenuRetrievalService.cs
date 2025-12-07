using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuRetrievalService
    {
        Task<Result<List<ItemResponse>>> GetAllItemsAPIAsync();
        Task<Result<List<Item>>> GetAllItemsMVCAsync();
        Task<Result<List<Category>>> GetCategoriesAsync();
        Task<Result<ItemResponse>> GetItemByIdAsyncAPI(int itemId);
        Task<Result<Item>> GetItemByIdAsyncMVC(int itemID);
        Result<List<Item>> GetItemsByCategoryId(int categoryId);
        Task<Result<List<TimeOfDay>>> GetTimeOfDaysAsync();
    }
}