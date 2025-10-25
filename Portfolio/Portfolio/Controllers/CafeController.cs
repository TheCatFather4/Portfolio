using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    public class CafeController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ISelectListBuilder _selectListBuilder;

        public CafeController(IMenuService menuService, ISelectListBuilder selectListBuilder)
        {
            _menuService = menuService;
            _selectListBuilder = selectListBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Menu()
        {
            var model = new MenuList();

            model.Categories = _selectListBuilder.BuildCategories(TempData);

            model.TimesOfDays = _selectListBuilder.BuildTimesOfDays(TempData);

            if (model.Categories == null || model.TimesOfDays == null)
            {
                TempData["Alert"] = Alert.CreateError("An error occurred. Please try again in a few minutes.");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Menu(MenuList model)
        {
            // Reload select lists
            model.Categories = _selectListBuilder.BuildCategories(TempData);
            model.TimesOfDays = _selectListBuilder.BuildTimesOfDays(TempData);

            var result = _menuService.GetAllItemsMVC();

            if (result.Ok)
            {
                model.Items = result.Data;
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                RedirectToAction("Index");
            }

            // filter by category
            if (model.SelectedCategoryID != null)
            {
                model.Items = model.Items
                    .Where(i => i.CategoryID == model.SelectedCategoryID)
                    .ToList();
            }

            // filter by time of day
            if (model.SelectedTimeOfDayID != null)
            {
                List<Item> filteredItems = new List<Item>();

                foreach (var item in model.Items)
                {
                    if (item.Prices != null)
                    {
                        List<ItemPrice> filteredPrices = new List<ItemPrice>();
                        foreach (var price in item.Prices)
                        {
                            if (price.TimeOfDayID == model.SelectedTimeOfDayID)
                            {
                                filteredPrices.Add(price);
                            }
                        }

                        if (filteredPrices.Any())
                        {
                            filteredItems.Add(new Item
                            {
                                ItemID = item.ItemID,
                                CategoryID = item.CategoryID,
                                ItemName = item.ItemName,
                                ItemDescription = item.ItemDescription,
                                Prices = filteredPrices,
                                ItemStatusID = item.ItemStatusID,
                                ItemImgPath = item.ItemImgPath
                            });
                        }
                    }
                }

                model.Items = filteredItems;
            }

            //filter by date
            if (model.Date != null)
            {
                DateTime selectedDate = new DateTime();

                var converted = DateTime.TryParse(model.Date, out selectedDate);

                if (!converted)
                {
                    TempData["Alert"] = Alert.CreateError("Date must be in MM/DD/YYYY format");
                    return RedirectToAction("Index");
                }

                List<Item> filteredDateItems = new List<Item>();

                foreach (var item in model.Items)
                {
                    if (item.Prices != null)
                    {
                        List<ItemPrice> filteredDatePrices = new List<ItemPrice>();
                        foreach (var price in item.Prices)
                        {
                            if ((price.StartDate <= selectedDate && price.EndDate == null) || 
                                (price.StartDate <= selectedDate && price.EndDate >= selectedDate))
                            {
                                filteredDatePrices.Add(price);
                            }
                        }

                        if (filteredDatePrices.Any())
                        {
                            filteredDateItems.Add(new Item
                            {
                                ItemID = item.ItemID,
                                CategoryID = item.CategoryID,
                                ItemName = item.ItemName,
                                ItemDescription = item.ItemDescription,
                                Prices= filteredDatePrices,
                                ItemStatusID = item.ItemStatusID,
                                ItemImgPath = item.ItemImgPath
                            });
                        }
                    }
                }

                model.Items = filteredDateItems;
            }

            return View(model);
        }

        public IActionResult OrderAPI()
        {
            return View();
        }
    }
}