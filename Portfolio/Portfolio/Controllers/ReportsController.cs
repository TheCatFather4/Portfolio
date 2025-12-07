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
        private readonly ISalesReportService _salesReportService;
        private readonly ISelectListBuilder _selectListBuilder;

        public ReportsController(ISalesReportService salesReportService, ISelectListBuilder selectListBuilder)
        {
            _salesReportService = salesReportService;
            _selectListBuilder = selectListBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ItemCategorySales()
        {
            var model = new ItemCategoryForm();

            model.Items = await _selectListBuilder.BuildItemsAsync(TempData);
            model.Categories = await _selectListBuilder.BuildCategoriesAsync(TempData);
            model.ItemReports = new List<ItemReport>();
            model.CategoryReports = new List<CategoryReport>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ItemCategorySales(ItemCategoryForm model)
        {
            model.Items = await _selectListBuilder.BuildItemsAsync(TempData);
            model.Categories = await _selectListBuilder.BuildCategoriesAsync(TempData);
            model.ItemReports = new List<ItemReport>();
            model.CategoryReports = new List<CategoryReport>();

            if (model.SelectedItemID.HasValue)
            {
                var filterItemResult = await _salesReportService.FilterItemsByItemIdAsync((int)model.SelectedItemID);

                if (filterItemResult.Ok)
                {
                    model.TotalQuantity = filterItemResult.Data.TotalQuantity;
                    model.TotalRevenue = filterItemResult.Data.TotalRevenue;

                    foreach (var idf in filterItemResult.Data.Items)
                    {
                        var idr = new ItemReport
                        {
                            Price = idf.Price,
                            Quantity = idf.Quantity,
                            ExtendedPrice = idf.ExtendedPrice,
                            DateSold = idf.DateSold
                        };

                        model.ItemReports.Add(idr);
                    }

                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError(filterItemResult.Message);
                return RedirectToAction("Index");
            }
            else if (model.SelectedCategoryID.HasValue)
            {
                var filterCategoryResult = await _salesReportService.FilterItemsByCategoryIdAsync(model.SelectedCategoryID.Value);

                if (filterCategoryResult.Ok)
                {
                    model.TotalQuantity = filterCategoryResult.Data.TotalQuantity;
                    model.TotalRevenue = filterCategoryResult.Data.TotalRevenue;

                    foreach (var crf in filterCategoryResult.Data.Categories)
                    {
                        var cr = new CategoryReport();
                        cr.ItemName = crf.ItemName;
                        cr.ItemDateReports = new List<ItemReport>();

                        foreach (var idrf in crf.ItemReports)
                        {
                            var idr = new ItemReport
                            {
                                Price = idrf.Price,
                                Quantity = idrf.Quantity,
                                ExtendedPrice = idrf.ExtendedPrice,
                                DateSold = idrf.DateSold
                            };

                            cr.ItemDateReports.Add(idr);
                        }

                        model.CategoryReports.Add(cr);
                    }

                    return View(model);
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(filterCategoryResult.Message);
                    return RedirectToAction("Index");
                }
            }

            TempData["Alert"] = Alert.CreateError("You must select an option.");
            return View(model);
        }

        [HttpGet]
        public IActionResult OrderSales()
        {
            var model = new OrderForm();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderSales(OrderForm model)
        {
            if (model.OrderDate != null)
            {
                var result = await _salesReportService.FilterOrdersByDateAsync((DateTime)model.OrderDate);

                if (result.Ok)
                {
                    model.Orders = result.Data.Orders;
                    model.TotalRevenue = result.Data.Revenue;

                    return View(model);
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return RedirectToAction("Index", "Reports");
                }
            }

            return View(model);
        }
    }
}