using Cafe.Core.Interfaces.Services;
using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Portfolio.Utilities
{
    public interface ISelectListBuilder
    {
        public SelectList BuildCategories(ITempDataDictionary tempData);
        public SelectList BuildTimesOfDays(ITempDataDictionary tempData);
        public SelectList BuildItems(ITempDataDictionary tempData);
        public SelectList BuildPaymentTypes(ITempDataDictionary tempData);
    }

    public class SelectListBuilder : ISelectListBuilder
    {
        private readonly IMenuService _menuService;
        private readonly IMVPaymentService _paymentService;

        public SelectListBuilder(IMenuService menuService, IMVPaymentService paymentService)
        {
            _menuService = menuService;
            _paymentService = paymentService;
        }

        public SelectList BuildCategories(ITempDataDictionary tempData)
        {
            var categoriesResult = _menuService.GetCategories();

            if (categoriesResult.Ok)
            {
                return new SelectList(categoriesResult.Data, "CategoryID", "CategoryName");
            }
            else
            {
                return null;
            }
        }

        public SelectList BuildItems(ITempDataDictionary tempData)
        {
            var itemsResult = _menuService.GetAllItems();

            if (itemsResult.Ok)
            {
                return new SelectList(itemsResult.Data, "ItemID", "ItemName");
            }
            else
            {
                return null;
            }
        }

        public SelectList BuildPaymentTypes(ITempDataDictionary tempData)
        {
            var paymentTypesResult = _paymentService.GetPaymentTypes();

            if (paymentTypesResult.Ok)
            {
                return new SelectList(paymentTypesResult.Data, "PaymentTypeID", "PaymentTypeName");
            }
            else
            {
                return null;
            }
        }

        public SelectList BuildTimesOfDays(ITempDataDictionary tempData)
        {
            var timesResult = _menuService.GetTimeOfDays();

            if (timesResult.Ok)
            {
                return new SelectList(timesResult.Data, "TimeOfDayID", "TimeOfDayName");
            }
            else
            {
                return null;
            }
        }
    }
}