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
        public IActionResult Orders()
        {
            var model = new OrderReportForm();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Orders(OrderReportForm model)
        {
            if (model.OrderDate != null)
            {
                var result = _salesReportService.FilterOrdersByDate((DateTime)model.OrderDate);

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

        [HttpGet]
        public IActionResult Items()
        {
            var model = new ItemReportForm();

            model.Items = _selectListBuilder.BuildItems(TempData);
            model.Categories = _selectListBuilder.BuildCategories(TempData);
            model.Dates = new List<ItemDateReport>();
            model.CategoryReports = new List<CategoryReport>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Items(ItemReportForm model)
        {
            model.Items = _selectListBuilder.BuildItems(TempData);
            model.Categories = _selectListBuilder.BuildCategories(TempData);
            model.Dates = new List<ItemDateReport>();
            model.CategoryReports = new List<CategoryReport>();

            if (model.SelectedItemID.HasValue)
            {
                var filterItemResult = await _salesReportService.FilterItemsByItemIdAsync((int)model.SelectedItemID);

                if (filterItemResult.Ok)
                {
                    model.TotalQuantity = filterItemResult.Data.TotalQuantity;
                    model.TotalRevenue = filterItemResult.Data.TotalRevenue;

                    foreach (var idf in filterItemResult.Data.Dates)
                    {
                        var idr = new ItemDateReport
                        {
                            Price = idf.Price,
                            Quantity = idf.Quantity,
                            ExtendedPrice = idf.ExtendedPrice,
                            DateSold = idf.DateSold
                        };

                        model.Dates.Add(idr);
                    }

                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError(filterItemResult.Message);
                return RedirectToAction("Index");
            }
            else if (model.SelectedCategoryID.HasValue)
            {
                var filterCategoryResult = await _salesReportService.FilterItemsByCategoryId(model.SelectedCategoryID.Value);

                if (filterCategoryResult.Ok)
                {
                    model.TotalQuantity = filterCategoryResult.Data.TotalQuantity;
                    model.TotalRevenue = filterCategoryResult.Data.TotalRevenue;

                    foreach (var crf in filterCategoryResult.Data.Categories)
                    {
                        var cr = new CategoryReport();
                        cr.ItemName = crf.ItemName;
                        cr.ItemDateReports = new List<ItemDateReport>();

                        foreach (var idrf in crf.ItemReports)
                        {
                            var idr = new ItemDateReport
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
    }
}