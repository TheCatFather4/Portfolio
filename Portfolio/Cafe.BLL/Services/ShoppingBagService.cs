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
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;

        public ShoppingBagService(ILogger<ShoppingBagService> logger, IShoppingBagRepository shoppingBagRepository, IMenuRetrievalRepository menuRetrievalRepository)
        {
            _logger = logger;
            _shoppingBagRepository = shoppingBagRepository;
            _menuRetrievalRepository = menuRetrievalRepository;
        }

        public async Task<Result> AddItemToShoppingBagAsync(AddItemRequest dto)
        {
            var sbi = new ShoppingBagItem
            {
                ShoppingBagID = dto.ShoppingBagId,
                ItemID = dto.ItemId,
                ItemStatusID = dto.ItemStatusId,
                Quantity = dto.Quantity,
                ItemName = dto.ItemName,
                Price = dto.Price,
                ItemImgPath = dto.ItemImgPath
            };

            try
            {
                await _shoppingBagRepository.AddItemToShoppingBagAsync(sbi);
                return ResultFactory.Success("Item successfully added to shopping bag!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when attempting to add {dto.ItemName} to Shopping Bag {dto.ShoppingBagId}: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result<ShoppingBagResponse>> GetShoppingBagByCustomerIdAsync(int customerId)
        {
            try
            {
                var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

                if (shoppingBag == null)
                {
                    _logger.LogError($"Shopping bag for Customer ID: {customerId} not found.");
                    return ResultFactory.Fail<ShoppingBagResponse>("An error occurred. Please try again in a few minutes.");
                }

                var dto = new ShoppingBagResponse
                {
                    ShoppingBagID = shoppingBag.ShoppingBagID,
                    CustomerID = shoppingBag.CustomerID,
                    Items = new List<ShoppingBagItemResponse>()
                };

                foreach (var item in shoppingBag.Items)
                {
                    var itemDto = new ShoppingBagItemResponse
                    {
                        ShoppingBagItemID = item.ShoppingBagItemID,
                        ShoppingBagID = item.ShoppingBagID,
                        ItemID = item.ItemID,
                        Quantity = item.Quantity,
                        ItemName = item.ItemName,
                        Price = (decimal)item.Price,
                        ItemImgPath = item.ItemImgPath
                    };

                    dto.Items.Add(itemDto);
                }

                return ResultFactory.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve shopping bag for Customer {customerId}: {ex.Message}");
                return ResultFactory.Fail<ShoppingBagResponse>("An error occurred. Please contact our customer service team.");
            }
        }

        public async Task<Result> RemoveItemFromShoppingBagAsync(int customerId, int shoppingBagItemId)
        {
            var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

            if (shoppingBag == null)
            {
                _logger.LogError("Shopping Bag not found.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            var itemToRemove = shoppingBag.Items
                .FirstOrDefault(i => i.ShoppingBagItemID == shoppingBagItemId);

            if (itemToRemove == null)
            {
                _logger.LogError("Item to remove from bag not found.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            try
            {
                await _shoppingBagRepository.RemoveItemFromShoppingBagAsync(itemToRemove);
                return ResultFactory.Success("Item successfully removed from shopping bag.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while attempting to remove an item from the shopping bag: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result> UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity)
        {
            try
            {
                await _shoppingBagRepository.UpdateItemQuantityAsync(shoppingBagItemId, quantity);
                return ResultFactory.Success("Item quantity successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while attempting to update item quantity {ex.Message}");
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

        public async Task<Result<Item>> GetItemWithPriceAsync(int itemId)
        {
            try
            {
                var item = await _menuRetrievalRepository.GetItemByIdAsync(itemId);

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
    }
}