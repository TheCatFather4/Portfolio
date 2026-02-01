using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Requests;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning shopping bags.
    /// </summary>
    public class ShoppingBagService : IShoppingBagService
    {
        private readonly ILogger _logger;
        private readonly IShoppingBagRepository _shoppingBagRepository;

        /// <summary>
        /// Constructs a service with the dependencies required to perform tasks related to shopping bags.
        /// </summary>
        /// <param name="logger">A dependency used for logging errors.</param>
        /// <param name="shoppingBagRepository">A dependency used for retrieving shopping bags, and adding, updating, and removing items.</param>
        public ShoppingBagService(ILogger<ShoppingBagService> logger, IShoppingBagRepository shoppingBagRepository)
        {
            _logger = logger;
            _shoppingBagRepository = shoppingBagRepository;
        }

        /// <summary>
        /// Maps a request DTO to a new instance of a ShoppingBagItem. 
        /// The item is then added to the database.
        /// </summary>
        /// <param name="dto">A DTO with data to add to the database.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
        public async Task<Result> AddItemToShoppingBagAsync(AddItemToBagRequest dto)
        {
            try
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

                await _shoppingBagRepository.AddItemToShoppingBagAsync(sbi);
                return ResultFactory.Success("Item successfully added to shopping bag!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when attempting to add {dto.ItemName} to Shopping Bag {dto.ShoppingBagId}: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        /// <summary>
        /// A CustomerID is used to retrieve a ShoppingBag record. 
        /// If successful, items are removed from the customer's shopping bag.
        /// </summary>
        /// <param name="customerId">A CustomerID used to retrieve a ShoppingBag record.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
        public async Task<Result> ClearShoppingBagAsync(int customerId)
        {
            try
            {
                var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

                if (shoppingBag == null)
                {
                    _logger.LogError("Shopping Bag not found.");
                    return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
                }

                await _shoppingBagRepository.ClearShoppingBagAsync(shoppingBag.ShoppingBagID);
                return ResultFactory.Success("Shopping bag successfully emptied.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to clear the shopping bag: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        /// <summary>
        /// A CustomerID is used to retrieve a ShoppingBag record.
        /// If successful, data is mapped to a response DTO and returned.
        /// </summary>
        /// <param name="customerId">A CustomerID used to retrieve a ShoppingBag record.</param>
        /// <returns>A Result DTO with a response DTO as its data.</returns>
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

        /// <summary>
        /// A ShoppingBagItemID is used to retrieve a ShoppingBagItem record.
        /// If successful, the data is returned.
        /// </summary>
        /// <param name="shoppingBagItemId">A ShoppingBagItemID used to retrieve a ShoppingBagItem record.</param>
        /// <returns>A Result DTO with a ShoppingBagItem record as its data.</returns>
        public async Task<Result<ShoppingBagItem>> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            try
            {
                var item = await _shoppingBagRepository.GetShoppingBagItemByIdAsync(shoppingBagItemId);

                if (item == null)
                {
                    _logger.LogError($"An error occurred when attempting to retrieve ShoppingBagItem with ID: {shoppingBagItemId}.");
                    return ResultFactory.Fail<ShoppingBagItem>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve a shopping bag item: {ex.Message}");
                return ResultFactory.Fail<ShoppingBagItem>("An error occurred. Please contact the site administrator.");
            }
        }

        /// <summary>
        /// A CustomerID is used to retrieve ShopingBag data.
        /// If successful, the data is filtered for a ShoppingBagItem to remove from the bag.
        /// If found, the item is removed from the bag.
        /// </summary>
        /// <param name="customerId">A CustomerID used to retrieve ShoppingBag data.</param>
        /// <param name="shoppingBagItemId">A ShoppingBagItemID used to filter ShoppingBagItem data.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
        public async Task<Result> RemoveItemFromShoppingBagAsync(int customerId, int shoppingBagItemId)
        {
            try
            {
                var shoppingBag = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

                if (shoppingBag == null)
                {
                    _logger.LogError($"ShoppingBag not found with CustomerID: {customerId}.");
                    return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
                }

                var itemToRemove = shoppingBag.Items?
                    .FirstOrDefault(i => i.ShoppingBagItemID == shoppingBagItemId);

                if (itemToRemove == null)
                {
                    _logger.LogError($"Item to remove not found with ShoppingBagItemID: {shoppingBagItemId}.");
                    return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
                }

                await _shoppingBagRepository.RemoveItemFromShoppingBagAsync(itemToRemove);
                return ResultFactory.Success("Item successfully removed from shopping bag.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while attempting to remove an item from the shopping bag: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        /// <summary>
        /// A ShoppingBagItemID and a quantity are used to update a ShoppingBagItem record associated with a specific customer.
        /// </summary>
        /// <param name="shoppingBagItemId">A ShoppingBagItemID used to select a specific record.</param>
        /// <param name="quantity">The quantity to update.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
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
    }
}