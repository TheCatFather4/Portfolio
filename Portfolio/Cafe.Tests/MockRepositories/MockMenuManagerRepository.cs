using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockMenuManagerRepository : IMenuManagerRepository
    {
        public void AddItem(Item item)
        {
            var items = new List<Item>();
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var item2 = new Item();
            item2 = item;
        }
    }
}