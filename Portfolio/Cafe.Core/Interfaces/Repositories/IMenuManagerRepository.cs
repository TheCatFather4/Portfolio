using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IMenuManagerRepository
    {
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
    }
}