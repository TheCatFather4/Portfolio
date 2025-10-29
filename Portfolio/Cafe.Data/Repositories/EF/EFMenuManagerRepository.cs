using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Data.Repositories.EF
{
    public class EFMenuManagerRepository : IMenuManagerRepository
    {
        private readonly CafeContext _dbContext;

        public EFMenuManagerRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public void AddItem(Item item)
        {
            _dbContext.Item.Add(item);
            _dbContext.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            _dbContext.Update(item);
            _dbContext.SaveChanges();
        }
    }
}