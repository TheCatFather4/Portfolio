using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class ShoppingBagService : IShoppingBagService
    {
        private readonly IShoppingBagRepository _shoppingBagRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger _logger;

        public ShoppingBagService(IShoppingBagRepository shoppingBagRepository, IMenuRepository menuRepository, ILogger logger)
        {
            _shoppingBagRepository = shoppingBagRepository;
            _menuRepository = menuRepository;
            _logger = logger;
        }

        public async Task<Result> AddItemToBagAsync(int customerId, int itemId, byte quantity)
        {
            if (quantity <= 0)
            {
                return ResultFactory.Fail("Quantity must be greater than zero.");
            }

            var item = await _menuRepository.GetItemAsync(itemId);
            if (item == null)
            {
                return ResultFactory.Fail("Item not found on the menu.");
            }

            var shoppingBagItem = new ShoppingBagItem
            {
                ItemID = itemId,
                Quantity = quantity
            };

            try
            {
                await _shoppingBagRepository.AddItemAsync(customerId, shoppingBagItem);

                return ResultFactory.Success("Item successfully added to shopping bag.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred when adding an item.");
                return ResultFactory.Fail($"An error occurred when adding the item: {ex.Message}");
            }
        }

        public async Task<Result> ClearShoppingBagAsync(int customerId)
        {
            var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

            if (shoppingBag == null)
            {
                return ResultFactory.Fail("Shopping bag not found");
            }

            try
            {
                await _shoppingBagRepository.ClearShoppingBag(shoppingBag.ShoppingBagID);
                return ResultFactory.Success("Shopping bag successfully emptied.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred when attempting to clear the shopping bag.");
                return ResultFactory.Fail($"An error occurred when attempting to clear shopping bag: {ex.Message}");
            }
        }

        public async Task<Result<ShoppingBag>> GetShoppingBagAsync(int customerId)
        {
            try
            {
                var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

                if (shoppingBag == null)
                {
                    return ResultFactory.Fail<ShoppingBag>("Shopping bag not found.");
                }

                return ResultFactory.Success(shoppingBag);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred when retrieving the shopping bag.");
                return ResultFactory.Fail<ShoppingBag>(ex.Message);
            }
        }

        public async Task<Result> RemoveItemFromBagAsync(int customerId, int shoppingBagItemId)
        {
            var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

            if (shoppingBag == null)
            {
                return ResultFactory.Fail("Shopping bag not found.");
            }

            var itemToRemove = shoppingBag.Items.FirstOrDefault(i => i.ShoppingBagItemID == shoppingBagItemId);

            if (itemToRemove == null)
            {
                return ResultFactory.Fail("Item not found in shopping bag.");
            }

            try
            {
                await _shoppingBagRepository.RemoveItemAsync(shoppingBag.ShoppingBagID, shoppingBagItemId);
                return ResultFactory.Success("Item successfully removed from shopping bag.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while attempting to remove an item from the shopping bag.");
                return ResultFactory.Fail($"An error occurred while removing the item: {ex.Message}");
            }
        }

        public async Task<Result> UpdateItemQuantityAsync(int customerId, int shoppingBagItemId, byte quantity)
        {
            if (quantity == 0)
            {
                return await RemoveItemFromBagAsync(customerId, shoppingBagItemId);
            }

            var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

            if (shoppingBag == null)
            {
                return ResultFactory.Fail("Shopping bag not found.");
            }

            var existingItem = shoppingBag.Items.FirstOrDefault(i => i.ShoppingBagItemID == shoppingBagItemId);

            if (existingItem == null)
            {
                return ResultFactory.Fail("Item not found in shopping bag.");
            }

            try
            {
                await _shoppingBagRepository.UpdateItemQuantityAsync(shoppingBag.ShoppingBagID, shoppingBagItemId, quantity);
                return ResultFactory.Success("Item quantity successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update item quantity: {quantity}");
                return ResultFactory.Fail($"An error occurred while updating item quantity {ex.Message}");
            }
        }
    }
}
