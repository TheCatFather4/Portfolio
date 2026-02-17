using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Cafe.Reports;
using Portfolio.Utilities;

namespace Portfolio.Controllers.Cafe
{
    /// <summary>
    /// Handles requests concerning sales reports.
    /// A accountant role is required to utilize these controller methods.
    /// </summary>
    [Authorize(Roles = "Accountant")]
    public class ReportsController : Controller
    {
        private readonly ISalesReportService _salesReportService;
        private readonly ISelectListBuilder _selectListBuilder;

        /// <summary>
        /// Constructs a controller with the required dependencies to access sales reports.
        /// </summary>
        /// <param name="salesReportService"></param>
        /// <param name="selectListBuilder"></param>
        public ReportsController(ISalesReportService salesReportService, ISelectListBuilder selectListBuilder)
        {
            _salesReportService = salesReportService;
            _selectListBuilder = selectListBuilder;
        }

        /// <summary>
        /// Takes the user to the main sales reports web page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Takes the user to a webpage where can they use two different forms to access sales reports.
        /// </summary>
        /// <returns>A created ViewResult object with the model state.</returns>
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

        /// <summary>
        /// Items are filtered in accord with the form the user selected.
        /// If the item form was chosen, the item's sales reports will be displayed to the user.
        /// If the category form was chosen, the category's sales reports will be displayed to the user.
        /// </summary>
        /// <param name="model">A model used to display sales data.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ItemCategorySales(ItemCategoryForm model)
        {
            // Select lists must be reloaded before returning model.
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

                    foreach (var idf in filterItemResult.Data.Reports)
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

                    foreach (var crf in filterCategoryResult.Data.CategoryItems)
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

        /// <summary>
        /// Takes the user to a web page that lets them use a form to filter sales by date.
        /// </summary>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpGet]
        public IActionResult OrderSales()
        {
            var model = new DateReportForm();

            return View(model);
        }

        /// <summary>
        /// Orders are filtered by the selected date and then displayed to the user.
        /// </summary>
        /// <param name="model">A model used to display order data by date.</param>
        /// <returns>A created ViewResult object with the model state.S</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderSales(DateReportForm model)
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