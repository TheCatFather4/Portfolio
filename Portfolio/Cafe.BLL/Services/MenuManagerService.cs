using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Filters;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning menu management tasks.
    /// </summary>
    public class MenuManagerService : IMenuManagerService
    {
        private readonly ILogger _logger;
        private readonly IMenuManagerRepository _menuManagerRepository;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;

        /// <summary>
        /// Constructs a service with the dependencies required for menu management tasks.
        /// </summary>
        /// <param name="logger">A dependency used for logging error messages.</param>
        /// <param name="menuManagerRepository">A dependency used for adding and updating Item records.</param>
        /// <param name="menuRetrievalRepository">A dependency used for retrieving Item records.</param>
        public MenuManagerService(ILogger<MenuManagerService> logger, IMenuManagerRepository menuManagerRepository, IMenuRetrievalRepository menuRetrievalRepository)
        {
            _logger = logger;
            _menuManagerRepository = menuManagerRepository;
            _menuRetrievalRepository = menuRetrievalRepository;
        }

        /// <summary>
        /// An IMenuManagerRepository method is invoked to add a new Item record.
        /// </summary>
        /// <param name="item">An Item entity to be added to the database.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
        public async Task<Result> AddNewItemAsync(Item item)
        {
            try
            {
                await _menuManagerRepository.AddItemAsync(item);
                return ResultFactory.Success($"{item.ItemName} successfully added to the menu!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add an item to the menu: {ex.Message}");
                return ResultFactory.Fail("An error ocurred. Please contact the administrator.");
            }
        }

        /// <summary>
        /// An IMenuRetrievalRepository method is invoked to retrieve all Item records. 
        /// If successful, the MenuFilter DTO is then checked for any values.
        /// If values are present, the List of Item records will filtered accordingly.
        /// If no values are present, all Item records will be returned.
        /// </summary>
        /// <param name="dto">Used to filter the List of Item entities returned from the repository.</param>
        /// <returns>A Result DTO with a List of Item entities as its data.</returns> 
        public async Task<Result<List<Item>>> FilterMenuAsync(MenuFilter dto)
        {
            try
            {
                var menu = await _menuRetrievalRepository.GetAllItemsAsync();

                if (menu.Count() == 0)
                {
                    _logger.LogError("Menu items not found.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please try again in a few minutes.");
                }

                // Filter records by Category
                if (dto.CategoryID != null)
                {
                    menu = menu
                        .Where(i => i.CategoryID == dto.CategoryID)
                        .ToList();
                }

                // Filter records by TimeOfDay
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

                // Filter records by Date
                if (dto.Date != null)
                {
                    var itemsByDate = new List<Item>();

                    foreach (var item in menu)
                    {
                        var pricesByDate = new List<ItemPrice>();

                        foreach (var price in item.Prices)
                        {
                            if ((price.StartDate <= dto.Date && price.EndDate == null) ||
                                (price.StartDate <= dto.Date && price.EndDate >= dto.Date))
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
        /// An IMenuManagerRepository method is invoked to update an Item record.
        /// </summary>
        /// <param name="item">The Item data to be send to the database.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
        public async Task<Result> UpdateItemAsync(Item item)
        {
            try
            {
                await _menuManagerRepository.UpdateItemAsync(item);
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