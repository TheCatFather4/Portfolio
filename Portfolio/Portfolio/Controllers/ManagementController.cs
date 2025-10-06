﻿using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagementController : Controller
    {
        private readonly IManagementService _managementService;
        private readonly IMenuService _menuService;
        private readonly ISelectListBuilder _selectListBuilder;

        public ManagementController(IManagementService managementService, IMenuService menuService, ISelectListBuilder selectListBuilder)
        {
            _managementService = managementService;
            _menuService = menuService;
            _selectListBuilder = selectListBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Server()
        {
            var model = new ServerList();

            var result = _managementService.GetServers();

            if (result.Ok)
            {
                model.AvailableServers = result.Data;
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditServer(int id)
        {
            var result = _managementService.GetServerById(id);

            if (result.Ok)
            {
                return View(new ServerForm(result.Data));
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditServer(int id, ServerForm model)
        {
            if (id != model.ServerID)
            {
                TempData["Alert"] = Alert.CreateError("Route mismatch");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();

                var result = _managementService.UpdateServer(entity);

                if (result.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess(result.Message);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateServer()
        {
            return View(new ServerForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateServer(ServerForm model)
        {
            if (ModelState.IsValid)
            {
                model.HireDate = DateTime.Now;

                var entity = model.ToEntity();

                var result = _managementService.AddServer(entity);

                if (result.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess(result.Message);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Menu()
        {
            var model = new List<MenuItem>();

            var result = _menuService.GetMenu();

            if (result.Ok)
            {
                foreach (var item in result.Data)
                {
                    var mapped = new MenuItem(item);
                    model.Add(mapped);
                }
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditMenu(int id)
        {
            var result = _managementService.GetMenuItemById(id);

            if (result.Ok)
            {
                return View(new ItemForm(result.Data));
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMenu(int id, ItemForm model)
        {
            if (id != model.ItemID)
            {
                TempData["Alert"] = Alert.CreateError("Route mismatch");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var item = model.ToEntity();

                var result = _managementService.UpdateMenu(item);

                if (result.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess(result.Message);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            var model = new AddItem();

            model.Categories = _selectListBuilder.BuildCategories(TempData);
            model.TimeOfDays = _selectListBuilder.BuildTimesOfDays(TempData);

            if (model.Categories == null || model.TimeOfDays == null)
            {
                TempData["Alert"] = Alert.CreateError("An error occurred. Please try again in a few minutes.");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateItem(AddItem model)
        {
            model.Categories = _selectListBuilder.BuildCategories(TempData);
            model.TimeOfDays = _selectListBuilder.BuildTimesOfDays(TempData);

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();

                var result = _managementService.AddItem(entity);

                if (result.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess(result.Message);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }

            return View(model);
        }
    }
}