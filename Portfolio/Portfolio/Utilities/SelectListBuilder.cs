using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Portfolio.Utilities
{
    public interface ISelectListBuilder
    {
        public Task<SelectList> BuildCategoriesAsync(ITempDataDictionary tempData);
        public Task<SelectList> BuildTimesOfDaysAsync(ITempDataDictionary tempData);
        public Task<SelectList> BuildItemsAsync(ITempDataDictionary tempData);
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

        public async Task<SelectList> BuildCategoriesAsync(ITempDataDictionary tempData)
        {
            var categoriesResult = await _menuService.GetCategoriesAsync();

            if (categoriesResult.Ok)
            {
                return new SelectList(categoriesResult.Data, "CategoryID", "CategoryName");
            }
            else
            {
                return null;
            }
        }

        public async Task<SelectList> BuildItemsAsync(ITempDataDictionary tempData)
        {
            var itemsResult = await _menuService.GetAllItemsMVCAsync();

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

        public async Task<SelectList> BuildTimesOfDaysAsync(ITempDataDictionary tempData)
        {
            var timesResult = await _menuService.GetTimeOfDaysAsync();

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