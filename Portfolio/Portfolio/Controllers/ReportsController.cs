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
        private readonly ISelectListBuilder _selectListBuilder;

        public ReportsController(IAccountantService accountantService, ISelectListBuilder selectListBuilder)
        {
            _accountantService = accountantService;
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
                        if (o.PaymentStatusID == 1)
                        {
                            revenue += o.SubTotal;
                        }
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
            model.Dates = new List<ItemDateReport>();
            model.CategoryReports = new List<CategoryReport>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Items(ItemRevenue model)
        {
            model.Items = _selectListBuilder.BuildItems(TempData);
            model.Categories = _selectListBuilder.BuildCategories(TempData);

            if (model.SelectedItemID.HasValue)
            {
                var itemPriceResult = _accountantService.GetItemPriceByItemId((int)model.SelectedItemID);

                if (itemPriceResult.Ok)
                {
                    var orderItemsResult = _accountantService.GetOrderItemsByItemPriceId((int)itemPriceResult.Data.ItemPriceID);

                    if (orderItemsResult.Ok)
                    {
                        var dateReportList = new List<ItemDateReport>();

                        foreach (var orderItem in orderItemsResult.Data)
                        {
                            model.TotalQuantity += orderItem.Quantity;
                            model.TotalRevenue += orderItem.ExtendedPrice;

                            dateReportList.Add(new ItemDateReport
                            {
                                Price = (decimal)itemPriceResult.Data.Price,
                                Quantity = orderItem.Quantity,
                                ExtendedPrice = orderItem.ExtendedPrice,
                                DateSold = orderItem.CafeOrder.OrderDate
                            });
                        }

                        model.Dates = dateReportList;
                        return View(model);
                    }

                    TempData["Alert"] = Alert.CreateInfo("There is no revenue for this item.");
                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError("An error occurred.");
                return RedirectToAction("Index");
            }
            else if (model.SelectedCategoryID.HasValue)
            {
                // 1. Get items
                var itemsResult = _accountantService.GetItemsByCategoryID((int)model.SelectedCategoryID);

                // 2a. if successful continue
                if (itemsResult.Ok)
                {
                    // 3. new CategoryReports List - this is the list of lists
                    var categoryReportList = new List<CategoryReport>();

                    int orderItems = 0;

                    // 4. loop through each item (from step 1)
                    foreach (var item in itemsResult.Data)
                    {
                        // for the current item being iterated.....

                        // 5. new Category report
                        var categoryReport = new CategoryReport();

                        // 6. map item name to the new report
                        categoryReport.ItemName = item.ItemName;

                        // 7. get item price
                        var itemPriceResult2 = _accountantService.GetItemPriceByItemId((int)item.ItemID); // add a variable to help

                        // 8a. if item price retrival is successful
                        if (itemPriceResult2.Ok)
                        {
                            // 9. get sold order items
                            var orderItemsResult2 = _accountantService.GetOrderItemsByItemPriceId((int)itemPriceResult2.Data.ItemPriceID);

                            // 10a. if sold item retrieval is successful
                            if (orderItemsResult2.Ok)
                            {
                                orderItems++;

                                // 11. create a new item date report - first report of many for the item
                                var dateReportList2 = new List<ItemDateReport>();

                                // 12. loop through sold items
                                foreach (var orderItem2 in orderItemsResult2.Data)
                                {
                                    model.TotalQuantity += orderItem2.Quantity;
                                    model.TotalRevenue += orderItem2.ExtendedPrice;

                                    // 13. map each sold item's data to a corresponding item date report - many ItemDateReports for ONE CategoryReport
                                    dateReportList2.Add(new ItemDateReport
                                    {
                                        Price = (decimal)itemPriceResult2.Data.Price,
                                        Quantity = orderItem2.Quantity,
                                        ExtendedPrice = orderItem2.ExtendedPrice,
                                        DateSold = orderItem2.CafeOrder.OrderDate
                                    });
                                }

                                // 14. map list of ItemDateReports to the Category report (that was instantiated at step 5)
                                categoryReport.ItemDateReports = dateReportList2;
                            }
                            else
                            {

                                // 10b. atleast one sold item in category
                                if (orderItems > 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    // 10c. no sold items in category
                                    model.CategoryReports = categoryReportList;
                                    TempData["Alert"] = Alert.CreateInfo("There is no revenue for this category.");
                                    return View(model);
                                }
                            }
                        }
                        else
                        {
                            // 8b. item prices not found
                            TempData["Alert"] = Alert.CreateError("An error occurred.");
                            return RedirectToAction("Index");
                        }

                        // 15. add category report to list of category reports
                        categoryReportList.Add(categoryReport);
                    }

                    // 16. After looping through each item, and all category reports are added to the list, map category list to model
                    model.CategoryReports = categoryReportList;

                    // 17. Return the model
                    return View(model);
                }

                // 2b. items not found
                TempData["Alert"] = Alert.CreateError("An error occurred.");
                return RedirectToAction("Index");
            }

            TempData["Alert"] = Alert.CreateError("You must select an option.");
            return View(model);
        }
    }
}