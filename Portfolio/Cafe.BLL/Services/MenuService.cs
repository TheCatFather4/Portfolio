using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class MenuService : IMenuService
    {
        private readonly ILogger _logger;
        private readonly IMenuRepository _menuRepository;

        public MenuService(ILogger<MenuService> logger, IMenuRepository menuRepository)
        {
            _logger = logger;
            _menuRepository = menuRepository;
        }

        public Result<List<Category>> GetCategories()
        {
            try
            {
                var categories = _menuRepository.GetCategories();

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

        public Result<List<TimeOfDay>> GetTimeOfDays()
        {
            try
            {
                var times = _menuRepository.GetTimeOfDays();

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

        public Result<List<Item>> GetMenu()
        {
            try
            {
                var items = _menuRepository.GetMenu();

                if (!items.Any())
                {
                    _logger.LogError("No menu items were found. Check the database connection.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please wait a few minutes and try again.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving the menu: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result<Item>> GetItem(int id)
        {
            try
            {
                var item = await _menuRepository.GetItemWithPriceAsync(id);

                if (item == null)
                {
                    _logger.LogError($"Item with ID: {id} was not found. Check the database connection.");
                    return ResultFactory.Fail<Item>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in attempting to retrieve an item: {ex.Message}");
                return ResultFactory.Fail<Item>("An error occurred. Please contact our management team.");
            }
        }

        public Result<List<Item>> GetItems()
        {
            try
            {
                var items = _menuRepository.GetItems();

                if (items.Count() == 0)
                {
                    _logger.LogError("Items not found in database. Check the database connection.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving meun items: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result<ItemPrice>> GetItemPriceByIdAsync(int itemId)
        {
            try
            {
                var itemPrice = await _menuRepository.GetItemPriceByIdAsync(itemId);

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
    }
}