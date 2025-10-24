using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class ShoppingBagService : IShoppingBagService
    {
        private readonly ILogger _logger;
        private readonly IShoppingBagRepository _shoppingBagRepository;
        private readonly IMenuRepository _menuRepository;

        public ShoppingBagService(ILogger<ShoppingBagService> logger, IShoppingBagRepository shoppingBagRepository, IMenuRepository menuRepository)
        {
            _logger = logger;
            _shoppingBagRepository = shoppingBagRepository;
            _menuRepository = menuRepository;
        }

        public async Task<Result> APIAddItemToBagAsync(int customerId, AddItemRequest dto)
        {
            if (dto.Quantity <= 0)
            {
                return ResultFactory.Fail("Quantity must be greater than zero.");
            }

            var item = await _menuRepository.GetItemByIdAsync(dto.ItemId);

            if (item == null)
            {
                _logger.LogError($"Item with ID: {dto.ItemId} not found.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            var shoppingBagItem = new ShoppingBagItem
            {
                ShoppingBagID = dto.ShoppingBagId,
                ItemID = dto.ItemId,
                Quantity = dto.Quantity,
                ItemName = item.ItemName,
                Price = item.Prices[0].Price,
                ItemImgPath = item.ItemImgPath
            };

            try
            {
                await _shoppingBagRepository.APIAddItemAsync(customerId, shoppingBagItem);

                return ResultFactory.Success("Item successfully added to shopping bag.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add an item: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result> ClearShoppingBagAsync(int customerId)
        {
            var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

            if (shoppingBag == null)
            {
                _logger.LogError("Shopping Bag not found.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            try
            {
                await _shoppingBagRepository.ClearShoppingBag(shoppingBag.ShoppingBagID);
                return ResultFactory.Success("Shopping bag successfully emptied.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to clear the shopping bag: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result<ShoppingBag>> GetShoppingBagAsync(int customerId)
        {
            try
            {
                var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

                if (shoppingBag == null)
                {
                    _logger.LogError("Shopping Bag not found.");
                    return ResultFactory.Fail<ShoppingBag>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(shoppingBag);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred when retrieving the shopping bag: {ex.Message}");
                return ResultFactory.Fail<ShoppingBag>("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result> RemoveItemFromBagAsync(int customerId, int shoppingBagItemId)
        {
            var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

            if (shoppingBag == null)
            {
                _logger.LogError("Shopping Bag not found.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            var itemToRemove = shoppingBag.Items.FirstOrDefault(i => i.ShoppingBagItemID == shoppingBagItemId);

            if (itemToRemove == null)
            {
                _logger.LogError("Item to remove from bag not found.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            try
            {
                await _shoppingBagRepository.RemoveItemAsync(shoppingBag.ShoppingBagID, shoppingBagItemId);
                return ResultFactory.Success("Item successfully removed from shopping bag.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while attempting to remove an item from the shopping bag: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
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
                _logger.LogError($"Shopping bag with Customer ID: {customerId} not found.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            var existingItem = shoppingBag.Items.FirstOrDefault(i => i.ShoppingBagItemID == shoppingBagItemId);

            if (existingItem == null)
            {
                _logger.LogError("Item not found in shopping bag.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            try
            {
                await _shoppingBagRepository.UpdateItemQuantityAsync(shoppingBag.ShoppingBagID, shoppingBagItemId, quantity);
                return ResultFactory.Success("Item quantity successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while attempting to update item quantity {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result> MVCAddItemToBagAsync(int shoppingBagId, int itemId, string itemName, decimal price, byte quantity, string imgPath)
        {
            var bagItem = new ShoppingBagItem
            {
                ShoppingBagID = shoppingBagId,
                ItemID = itemId,
                Quantity = quantity,
                ItemName = itemName,
                Price = price,
                ItemImgPath = imgPath
            };

            try
            {
                await _shoppingBagRepository.MVCAddItemAsync(bagItem);
                return ResultFactory.Success("Item successfully added to cart!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add an item to the bag: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team for assistance.");
            }
        }

        public async Task<Result<Item>> GetItemWithPriceAsync(int itemId)
        {
            try
            {
                var item = await _menuRepository.GetItemByIdAsync(itemId);

                if (item != null)
                {
                    return ResultFactory.Success(item);
                }

                _logger.LogError($"Item with id: {itemId} not found.");
                return ResultFactory.Fail<Item>("An error occurred. Please try again in a few minutes.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve an item: {ex.Message}");
                return ResultFactory.Fail<Item>("An error occurred. Please contact our management team for assistance.");
            }
        }

        public async Task<Result<ShoppingBagItem>> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            var item = await _shoppingBagRepository.GetShoppingBagItemByIdAsync(shoppingBagItemId);

            if (item != null)
            {
                return ResultFactory.Success(item);
            }

            _logger.LogError($"An error occurred when attempting to retrieve a shopping bag item.");
            return ResultFactory.Fail<ShoppingBagItem>("An error occurred. Please try again in a few minutes.");
        }

        public async Task<Result<ShoppingBagResponse>> APIGetShoppingBagAsync(int customerId)
        {
            try
            {
                var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

                if (shoppingBag == null)
                {
                    _logger.LogError("Shopping Bag not found.");
                    return ResultFactory.Fail<ShoppingBagResponse>("An error occurred. Please try again in a few minutes.");
                }

                var sbr = new ShoppingBagResponse();
                sbr.ShoppingBagID = shoppingBag.ShoppingBagID;
                sbr.CustomerID = shoppingBag.CustomerID;
                sbr.Items = new List<ShoppingBagItemResponse>();

                foreach (var item in shoppingBag.Items)
                {
                    var sbri = new ShoppingBagItemResponse();
                    sbri.ShoppingBagItemID = item.ShoppingBagItemID;
                    sbri.ItemID = item.ItemID;
                    sbri.ItemName = item.ItemName;
                    sbri.Quantity = item.Quantity;
                    sbri.Price = (decimal)item.Price;

                    sbr.Items.Add(sbri);
                }

                return ResultFactory.Success(sbr);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred when retrieving the shopping bag: {ex.Message}");
                return ResultFactory.Fail<ShoppingBagResponse>("An error occurred. Please contact our management team.");
            }
        }
    }
}