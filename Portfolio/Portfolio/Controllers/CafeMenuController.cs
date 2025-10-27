using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    public class CafeMenuController : Controller
    {
        private readonly IMenuManagerService _menuManagerService;
        private readonly ISelectListBuilder _selectListBuilder;

        public CafeMenuController(IMenuManagerService menuManagerService, ISelectListBuilder selectListBuilder)
        {
            _menuManagerService = menuManagerService;
            _selectListBuilder = selectListBuilder;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new MenuForm();

            model.Categories = _selectListBuilder.BuildCategories(TempData);

            model.TimesOfDays = _selectListBuilder.BuildTimesOfDays(TempData);

            if (model.Categories == null || model.TimesOfDays == null)
            {
                TempData["Alert"] = Alert.CreateError("An error occurred. Please try again in a few minutes.");
                return RedirectToAction("Cafe", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MenuForm model)
        {
            // Reload select lists
            model.Categories = _selectListBuilder.BuildCategories(TempData);
            model.TimesOfDays = _selectListBuilder.BuildTimesOfDays(TempData);

            var dto = new MenuFilter
            {
                CategoryID = model.SelectedCategoryID,
                TimeOfDayID = model.SelectedTimeOfDayID,
                Date = model.Date
            };

            var result = _menuManagerService.FilterMenu(dto);

            if (result.Ok)
            {
                model.Items = result.Data;
            }

            return View(model);
        }
    }
}