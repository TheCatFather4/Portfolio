using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Reports;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class ReportsController : Controller
    {
        private readonly IAccountantService _accountantService;
        private readonly IMenuService _menuService;
        private readonly ISelectListBuilder _selectListBuilder;

        public ReportsController(IAccountantService accountantService, IMenuService menuService, ISelectListBuilder selectListBuilder)
        {
            _accountantService = accountantService;
            _menuService = menuService;
            _selectListBuilder = selectListBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var model = new OrderRevenue();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Orders(OrderRevenue model)
        {
            if (model.OrderDate != null)
            {
                var result = _accountantService.GetOrders();

                if (result.Ok)
                {
                    var orders = result.Data
                        .Where(o => o.OrderDate.Date == model.OrderDate.Value.Date)
                        .ToList();

                    model.Orders = orders;

                    decimal revenue = 0.00M;

                    foreach (var o in orders)
                    {
                        revenue += o.SubTotal;
                    }

                    model.TotalRevenue = revenue;

                    return View(model);
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError("Error getting orders");
                    return RedirectToAction("Index", "Reports");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Items()
        {
            var model = new ItemRevenue();

            model.Items = _selectListBuilder.BuildItems(TempData);
            model.Categories = _selectListBuilder.BuildCategories(TempData);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Items(ItemRevenue model)
        {
            model.Items = _selectListBuilder.BuildItems(TempData);
            model.Categories = _selectListBuilder.BuildCategories(TempData);

            var result = _accountantService.GetItemPrices();

            if (result.Ok)
            {
                var filteredData = result.Data;

                if (model.SelectedItemID.HasValue)
                {
                    filteredData = filteredData.Where(ip => ip.ItemID == model.SelectedItemID).ToList();
                }
                else if (model.SelectedCategoryID.HasValue)
                {
                    var itemIDsForCategoryResult = _accountantService.GetItemsByCategoryID(model.SelectedCategoryID.Value);

                    if (itemIDsForCategoryResult.Ok)
                    {
                        var itemIDs = itemIDsForCategoryResult.Data.Select(i => i.ItemID).ToList();

                        filteredData = filteredData.Where(ip => ip.ItemID.HasValue && itemIDs.Contains(ip.ItemID.Value)).ToList();
                    }
                }

                var itemsResult = _menuService.GetMenu();

                if (itemsResult.Ok)
                {
                    var processedPrices = new List<ItemPriceReport>();

                    foreach (var itemPrice in filteredData)
                    {
                        var totalQuantity = itemPrice.OrderItems.Sum(oi => oi.Quantity);
                        var totalExtendedPrice = itemPrice.OrderItems.Sum(oi => oi.ExtendedPrice);

                        processedPrices.Add(new ItemPriceReport
                        {
                            Item = itemsResult.Data.FirstOrDefault(i => i.ItemID == itemPrice.ItemID),
                            ItemPrice = itemPrice,
                            TotalQuantity = totalQuantity,
                            TotalExtendedPrice = totalExtendedPrice,
                        });
                    }
                    model.Prices = processedPrices;

                    model.TotalRevenue = processedPrices.Sum(ipr => ipr.TotalExtendedPrice);
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError("Unable to find item name.");
                    return RedirectToAction("Index", "Reports");
                }
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index", "Reports");
            }

            return View(model);
        }
    }
}
