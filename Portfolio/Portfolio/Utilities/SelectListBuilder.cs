using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Portfolio.Utilities
{
    /// <summary>
    /// An interface for the select list builder.
    /// </summary>
    public interface ISelectListBuilder
    {
        /// <summary>
        /// Builds a select list of categories.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public Task<SelectList> BuildCategoriesAsync(ITempDataDictionary tempData);

        /// <summary>
        /// Builds a select list of time of days.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public Task<SelectList> BuildTimesOfDaysAsync(ITempDataDictionary tempData);

        /// <summary>
        /// Builds a select list of items.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public Task<SelectList> BuildItemsAsync(ITempDataDictionary tempData);

        /// <summary>
        /// Builds a select list of payment types.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public Task<SelectList> BuildPaymentTypesAsync(ITempDataDictionary tempData);
    }

    /// <summary>
    /// Implements the ISelectBuilder interface.
    /// </summary>
    public class SelectListBuilder : ISelectListBuilder
    {
        private readonly IMenuRetrievalService _menuService;
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Constructs a select list builder with the required service dependencies.
        /// </summary>
        /// <param name="menuService">An implementation of the menu retrieval service interface.</param>
        /// <param name="paymentService">An implementation of the payment service interface.</param>
        public SelectListBuilder(IMenuRetrievalService menuService, IPaymentService paymentService)
        {
            _menuService = menuService;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Builds a select list of categories.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns>A select list</returns>
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

        /// <summary>
        /// Builds a select list of items.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns>A select list</returns>
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

        /// <summary>
        /// Builds a select list of payment types.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns>A select list</returns>
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

        /// <summary>
        /// Builds a select list of time of days.
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns>A select list</returns>
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