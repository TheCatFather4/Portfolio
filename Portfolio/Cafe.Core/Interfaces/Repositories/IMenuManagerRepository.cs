using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IMenuManagerRepository
    {
        void AddItem(Item item);
        void UpdateItem(Item item);
    }
}