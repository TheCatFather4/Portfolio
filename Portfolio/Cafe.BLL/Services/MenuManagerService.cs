using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles business logic concerning menu management
    /// </summary>
    public class MenuManagerService : IMenuManagerService
    {
        private readonly ILogger _logger;
        private readonly IMenuManagerRepository _menuManagerRepository;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;

        public MenuManagerService(ILogger<MenuManagerService> logger, IMenuManagerRepository menuManagerRepository, IMenuRetrievalRepository menuRetrievalRepository)
        {
            _logger = logger;
            _menuManagerRepository = menuManagerRepository;
            _menuRetrievalRepository = menuRetrievalRepository;
        }

        /// <summary>
        /// Handles logic involving adding a new item to the database
        /// </summary>
        /// <param name="item">An item entity</param>
        /// <returns>A result dto</returns>
        public Result AddNewItem(Item item)
        {
            try
            {
                _menuManagerRepository.AddItem(item);
                return ResultFactory.Success($"{item.ItemName} successfully added to the menu!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add an item to the menu: {ex.Message}");
                return ResultFactory.Fail("An error ocurred. Please contact the administrator.");
            }
        }

        /// <summary>
        /// Filters through all the items by category, time of day, and/or by date.
        /// </summary>
        /// <param name="dto">A dto used for filtering and mapping items</param>
        /// <returns>A result dto with a list of items and their itemprices</returns>
        public Result<List<Item>> FilterMenu(MenuFilter dto)
        {
            try
            {
                var menu = _menuRetrievalRepository.GetAllItems();

                if (menu.Count() == 0)
                {
                    _logger.LogError("Menu items not found.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please try again in a few minutes.");
                }

                // filter by category
                if (dto.CategoryID != null)
                {
                    menu = menu
                        .Where(i => i.CategoryID == dto.CategoryID)
                        .ToList();
                }

                // filter by time of day
                if (dto.TimeOfDayID != null)
                {
                    var itemsByTimeOfDay = new List<Item>();

                    foreach (var item in menu)
                    {
                        var pricesByTimeOfDay = new List<ItemPrice>();

                        foreach (var price in item.Prices)
                        {
                            if (price.TimeOfDayID == dto.TimeOfDayID)
                            {
                                pricesByTimeOfDay.Add(price);
                            }
                        }

                        if (pricesByTimeOfDay.Count() != 0)
                        {
                            var itemByTimeOfDay = new Item
                            {
                                ItemID = item.ItemID,
                                CategoryID = item.CategoryID,
                                ItemStatusID = item.ItemStatusID,
                                ItemName = item.ItemName,
                                ItemDescription = item.ItemDescription,
                                ItemImgPath = item.ItemImgPath,
                                Prices = pricesByTimeOfDay
                            };

                            itemsByTimeOfDay.Add(itemByTimeOfDay);
                        }
                    }

                    menu = itemsByTimeOfDay;
                }

                // filter by date
                if (dto.Date != null)
                {
                    var itemsByDate = new List<Item>();

                    foreach (var item in menu)
                    {
                        var pricesByDate = new List<ItemPrice>();

                        foreach (var price in item.Prices)
                        {
                            if (price.StartDate <= dto.Date && price.EndDate == null ||
                                price.StartDate <= dto.Date && price.EndDate >= dto.Date)
                            {
                                pricesByDate.Add(price);
                            }

                            if (pricesByDate.Count() != 0)
                            {
                                var itemByDate = new Item
                                {
                                    ItemID = item.ItemID,
                                    CategoryID = item.CategoryID,
                                    ItemStatusID = item.ItemStatusID,
                                    ItemName = item.ItemName,
                                    ItemDescription = item.ItemDescription,
                                    ItemImgPath = item.ItemImgPath,
                                    Prices = pricesByDate
                                };

                                itemsByDate.Add(itemByDate);
                            }
                        }
                    }

                    menu = itemsByDate;
                }

                return ResultFactory.Success(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred when attempting to filter menu: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact our management team.");
            }
        }

        /// <summary>
        /// Handles logic involving updating a current item record
        /// </summary>
        /// <param name="item">An item entity</param>
        /// <returns>A result dto</returns>
        public Result UpdateItem(Item item)
        {
            try
            {
                _menuManagerRepository.UpdateItem(item);
                return ResultFactory.Success("Item successfully updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred when attempting to update item.");
                return ResultFactory.Fail("An error occurred. Please contact the site administrator.");
            }
        }
    }
}