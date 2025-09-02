using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class ShoppingBagRepository : IShoppingBagRepository
    {
        private readonly CafeContext _dbContext;

        public ShoppingBagRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task AddItemAsync(int customerId, ShoppingBagItem item)
        {
            var shoppingBag = await _dbContext.ShoppingBag
                .Include(sb => sb.Items)
                .FirstOrDefaultAsync(sb => sb.CustomerID == customerId);

            if (shoppingBag == null)
            {
                shoppingBag = new ShoppingBag { CustomerID = customerId };
                await _dbContext.ShoppingBag.AddAsync(shoppingBag);
                await _dbContext.SaveChangesAsync();
            }

            var existingItem = shoppingBag.Items.FirstOrDefault(i => i.ItemID == item.ItemID);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                shoppingBag.Items.Add(item);
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
