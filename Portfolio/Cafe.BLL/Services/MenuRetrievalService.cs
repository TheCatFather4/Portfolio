using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class MenuRetrievalService : IMenuService
    {
        private readonly ILogger _logger;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;

        public MenuRetrievalService(ILogger<MenuRetrievalService> logger, IMenuRetrievalRepository menuRetrievalRepository)
        {
            _logger = logger;
            _menuRetrievalRepository = menuRetrievalRepository;
        }

        public Result<List<ItemResponse>> GetAllItemsAPI()
        {
            try
            {
                var menu = _menuRetrievalRepository.GetAllItems();

                if (menu != null)
                {
                    var items = new List<ItemResponse>();

                    foreach (var item in menu)
                    {
                        var ir = new ItemResponse();
                        ir.ItemID = (int)item.ItemID;
                        ir.CategoryID = (int)item.CategoryID;
                        ir.ItemName = item.ItemName;
                        ir.ItemDescription = item.ItemDescription;
                        ir.Prices = new List<ItemPriceResponse>();

                        foreach (var price in item.Prices)
                        {
                            var ipr = new ItemPriceResponse();
                            ipr.ItemPriceID = (int)price.ItemPriceID;
                            ipr.TimeOfDayID = (int)price.TimeOfDayID;
                            ipr.Price = (decimal)price.Price;
                            ipr.StartDate = (DateTime)price.StartDate;
                            ipr.EndDate = price.EndDate;

                            ir.Prices.Add(ipr);
                        }

                        items.Add(ir);
                    }

                    return ResultFactory.Success(items);
                }

                _logger.LogError("The menu was not found. Check database connection.");
                return ResultFactory.Fail<List<ItemResponse>>("An error occurred. Please try again in a few minutes.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred when attempting to retrieve the menu.");
                return ResultFactory.Fail<List<ItemResponse>>("An error occurred. Please contact the customer assistance team.");
            }
        }

        public Result<List<Item>> GetAllItemsMVC()
        {
            try
            {
                var items = _menuRetrievalRepository.GetAllItems();

                if (items.Count() == 0)
                {
                    _logger.LogError("Items not found in database. Check the database connection.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving menu items: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact our management team.");
            }
        }

        public Result<List<Category>> GetCategories()
        {
            try
            {
                var categories = _menuRetrievalRepository.GetCategories();

                if (categories.Count() == 0)
                {
                    _logger.LogError("No categories were found. Check the database connection.");
                    return ResultFactory.Fail<List<Category>>("An error occurred. Please wait a few minutes and try again.");
                }

                return ResultFactory.Success(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in attempting to get categories: {ex.Message}");
                return ResultFactory.Fail<List<Category>>("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result<ItemResponse>> GetItemByIdAsyncAPI(int itemId)
        {
            try
            {
                var item = await _menuRetrievalRepository.GetItemByIdAsync(itemId);

                if (item == null)
                {
                    _logger.LogError($"Item with ID: {itemId} was not found. Check the database connection.");
                    return ResultFactory.Fail<ItemResponse>("An error occurred. Please try again in a few minutes.");
                }

                var ir = new ItemResponse();
                ir.ItemID = (int)item.ItemID;
                ir.CategoryID = (int)item.CategoryID;
                ir.ItemName = item.ItemName;
                ir.ItemDescription = item.ItemDescription;
                ir.Prices = new List<ItemPriceResponse>();

                foreach (var price in item.Prices)
                {
                    var ipr = new ItemPriceResponse();
                    ipr.ItemPriceID = (int)price.ItemPriceID;
                    ipr.TimeOfDayID = (int)price.TimeOfDayID;
                    ipr.Price = (decimal)price.Price;
                    ipr.StartDate = (DateTime)price.StartDate;
                    ipr.EndDate = price.EndDate;

                    ir.Prices.Add(ipr);
                }

                return ResultFactory.Success(ir);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in attempting to retrieve an item: {ex.Message}");
                return ResultFactory.Fail<ItemResponse>("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result<Item>> GetItemByIdAsyncMVC(int itemID)
        {
            try
            {
                var item = await _menuRetrievalRepository.GetItemByIdAsync(itemID);

                if (item == null)
                {
                    _logger.LogError($"Item with id: {itemID} not found.");
                    return ResultFactory.Fail<Item>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when retrieving a menu item: {ex.Message}");
                return ResultFactory.Fail<Item>("An error occurred. Please contact the administrator.");
            }
        }

        public async Task<Result<ItemPrice>> GetItemPriceByItemIdAsync(int itemId)
        {
            try
            {
                var itemPrice = await _menuRetrievalRepository.GetItemPriceByItemIdAsync(itemId);

                if (itemPrice == null)
                {
                    _logger.LogError($"Item price not found for Item ID: {itemId}. Check the database connection.");
                    return ResultFactory.Fail<ItemPrice>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(itemPrice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                return ResultFactory.Fail<ItemPrice>("An error occurred. Please contact our management team.");
            }
        }

        public Result<List<Item>> GetItemsByCategoryId(int categoryId)
        {
            try
            {
                var items = _menuRetrievalRepository.GetItemsByCategoryId(categoryId);

                if (items.Count() == 0)
                {
                    _logger.LogError($"Items with category id: {categoryId} not found.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving items by category: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact the administrator.");
            }
        }

        public Result<List<TimeOfDay>> GetTimeOfDays()
        {
            try
            {
                var times = _menuRetrievalRepository.GetTimeOfDays();

                if (times.Count() == 0)
                {
                    _logger.LogError("No times of day were found. Check the database connection.");
                    return ResultFactory.Fail<List<TimeOfDay>>("An error occurred. Please wait a few minutes and try again.");
                }

                return ResultFactory.Success(times);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in attempting to get times of day: {ex.Message}");
                return ResultFactory.Fail<List<TimeOfDay>>("An error occurred. Please contact our management team.");
            }
        }
    }
}