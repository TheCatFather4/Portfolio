using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class EFShoppingBagRepository : IShoppingBagRepository
    {
        private readonly CafeContext _dbContext;

        public EFShoppingBagRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task AddItemToShoppingBagAsync(ShoppingBagItem item)
        {
            var existingItem = await _dbContext.ShoppingBagItem
                .FirstOrDefaultAsync(sbi => sbi.ItemID == item.ItemID && sbi.ShoppingBagID == item.ShoppingBagID);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                await _dbContext.ShoppingBagItem.AddAsync(item);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearShoppingBag(int shoppingBagId)
        {
            var items = await _dbContext.ShoppingBagItem
                .Where(sbi => sbi.ShoppingBagID == shoppingBagId)
                .ToListAsync();

            _dbContext.ShoppingBagItem.RemoveRange(items);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingBag> GetShoppingBagAsync(int customerId)
        {
            return await _dbContext.ShoppingBag
                .Include(sb => sb.Items)
                .FirstOrDefaultAsync(sb => sb.CustomerID == customerId);
        }

        public async Task<ShoppingBagItem> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            return await _dbContext.ShoppingBagItem
                .FirstOrDefaultAsync(sbi => sbi.ShoppingBagItemID == shoppingBagItemId);
        }

        public async Task RemoveItemAsync(int shoppingBagId, int shoppingBagItemId)
        {
            var itemToRemove = await _dbContext.ShoppingBagItem
                .FirstOrDefaultAsync(sbi => sbi.ShoppingBagItemID == shoppingBagItemId && sbi.ShoppingBagID == shoppingBagId);

            if (itemToRemove != null)
            {
                _dbContext.ShoppingBagItem.Remove(itemToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateItemQuantityAsync(int shoppingBagId, int shoppingBagItemId, byte quantity)
        {
            var itemToUpdate = await _dbContext.ShoppingBagItem
                .FirstOrDefaultAsync(sbi => sbi.ShoppingBagItemID == shoppingBagItemId && sbi.ShoppingBagID == shoppingBagId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}