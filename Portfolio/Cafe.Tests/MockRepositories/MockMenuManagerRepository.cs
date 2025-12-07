using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockMenuManagerRepository : IMenuManagerRepository
    {
        public async Task AddItemAsync(Item item)
        {
            var items = new List<Item>();

            await Task.Delay(1000);
            items.Add(item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            var existingItem = new Item();

            await Task.Delay(1000);
            existingItem = item;
        }
    }
}