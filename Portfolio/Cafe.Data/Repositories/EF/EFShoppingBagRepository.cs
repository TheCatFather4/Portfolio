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

        public async Task ClearShoppingBagAsync(int shoppingBagId)
        {
            var items = await _dbContext.ShoppingBagItem
                .Where(sbi => sbi.ShoppingBagID == shoppingBagId)
                .ToListAsync();

            _dbContext.ShoppingBagItem.RemoveRange(items);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag)
        {
            _dbContext.ShoppingBag.Add(shoppingBag);
            await _dbContext.SaveChangesAsync();

            return shoppingBag.ShoppingBagID;
        }

        public async Task<ShoppingBag?> GetShoppingBagAsync(int customerId)
        {
            return await _dbContext.ShoppingBag
                .Include(sb => sb.Items)
                .FirstOrDefaultAsync(sb => sb.CustomerID == customerId);
        }

        public async Task<ShoppingBagItem?> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            return await _dbContext.ShoppingBagItem
                .FirstOrDefaultAsync(sbi => sbi.ShoppingBagItemID == shoppingBagItemId);
        }

        public async Task<decimal> GetShoppingBagTotalAsync(int customerId)
        {
            var total = await _dbContext.ShoppingBag
                .Where(sb => sb.CustomerID == customerId)
                .SelectMany(sb => sb.Items, (sb, sbi) => sbi.Price * sbi.Quantity)
                .SumAsync();

            return (decimal)total;
        }

        public async Task RemoveItemFromShoppingBagAsync(ShoppingBagItem item)
        {
            _dbContext.ShoppingBagItem.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity)
        {
            var itemToUpdate = await _dbContext.ShoppingBagItem
                .FirstOrDefaultAsync(sbi => sbi.ShoppingBagItemID == shoppingBagItemId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}