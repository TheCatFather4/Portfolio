using Cafe.Core.DTOs.Filters;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Cafe.CafeMenu;
using Portfolio.Utilities;

namespace Portfolio.Controllers.Cafe
{
    /// <summary>
    /// Handles requests concerning the café menu.
    /// </summary>
    public class CafeMenuController : Controller
    {
        private readonly IMenuManagerService _menuManagerService;
        private readonly ISelectListBuilder _selectListBuilder;

        /// <summary>
        /// Constructs an MVC controller with the required dependencies for accessing the menu.
        /// </summary>
        /// <param name="menuManagerService"></param>
        /// <param name="selectListBuilder"></param>
        public CafeMenuController(IMenuManagerService menuManagerService, ISelectListBuilder selectListBuilder)
        {
            _menuManagerService = menuManagerService;
            _selectListBuilder = selectListBuilder;
        }

        /// <summary>
        /// Takes the user to a web page that allows them to select how they'd like to view the menu.
        /// Two select lists are loaded to allow for filtering menu items.
        /// </summary>
        /// <returns>A web page or an error message depending on the result.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new MenuForm();

            model.Categories = await _selectListBuilder.BuildCategoriesAsync(TempData);

            model.TimesOfDays = await _selectListBuilder.BuildTimesOfDaysAsync(TempData);

            if (model.Categories == null || model.TimesOfDays == null)
            {
                TempData["Alert"] = Alert.CreateError("An error occurred. Please try again in a few minutes.");
                return RedirectToAction("Cafe", "Home");
            }

            return View(model);
        }

        /// <summary>
        /// Filters the menu based upon what the user selected in the menu form.
        /// </summary>
        /// <param name="model">A model used to filter the café's menu.</param>
        /// <returns>The model with the filtered results. If no filter was chosen, all menu items are displayed.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MenuForm model)
        {
            // The select lists must be reloaded before returning the model again.
            model.Categories = await _selectListBuilder.BuildCategoriesAsync(TempData);
            model.TimesOfDays = await _selectListBuilder.BuildTimesOfDaysAsync(TempData);

            var dto = new MenuFilter
            {
                CategoryID = model.SelectedCategoryID,
                TimeOfDayID = model.SelectedTimeOfDayID,
                Date = model.Date
            };

            var result = await _menuManagerService.FilterMenuAsync(dto);

            if (result.Ok)
            {
                model.Items = result.Data;
            }

            return View(model);
        }
    }
}