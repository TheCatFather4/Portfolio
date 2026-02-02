using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuRetrievalService
    {
        Task<Result<List<ItemResponse>>> GetAllItemsAPIAsync();
        Task<Result<List<Item>>> GetAllItemsMVCAsync();
        Task<Result<List<Category>>> GetCategoriesAsync();
        Task<Result<ItemResponse>> GetItemByIdAPIAsync(int itemId);
        Task<Result<Item>> GetItemByIdMVCAsync(int itemID);
        Task<Result<List<TimeOfDay>>> GetTimeOfDaysAsync();
    }
}