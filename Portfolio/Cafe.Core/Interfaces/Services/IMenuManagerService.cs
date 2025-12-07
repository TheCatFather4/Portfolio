using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuManagerService
    {
        Task<Result> AddNewItemAsync(Item item);
        Task<Result<List<Item>>> FilterMenuAsync(MenuFilter dto);
        Task<Result> UpdateItemAsync(Item item);
    }
}