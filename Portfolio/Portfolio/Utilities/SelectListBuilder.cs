using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Portfolio.Utilities
{
    public interface ISelectListBuilder
    {
        public SelectList BuildCategories(ITempDataDictionary tempData);
        public SelectList BuildTimesOfDays(ITempDataDictionary tempData);
        public SelectList BuildItems(ITempDataDictionary tempData);
        public Task<SelectList> BuildPaymentTypesAsync(ITempDataDictionary tempData);
    }

    public class SelectListBuilder : ISelectListBuilder
    {
        private readonly IMenuRetrievalService _menuService;
        private readonly IPaymentService _paymentService;

        public SelectListBuilder(IMenuRetrievalService menuService, IPaymentService paymentService)
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
            var itemsResult = _menuService.GetAllItemsMVC();

            if (itemsResult.Ok)
            {
                return new SelectList(itemsResult.Data, "ItemID", "ItemName");
            }
            else
            {
                return null;
            }
        }

        public async Task<SelectList> BuildPaymentTypesAsync(ITempDataDictionary tempData)
        {
            var result = await _paymentService.GetPaymentTypesAsync();

            if (result.Ok)
            {
                return new SelectList(result.Data, "PaymentTypeID", "PaymentTypeName");
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