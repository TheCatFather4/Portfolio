using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Cafe.Management;
using Portfolio.Utilities;

namespace Portfolio.Controllers.Cafe
{
    /// <summary>
    /// Handles requests concerning management of menu and servers.
    /// A manager role is required to utilize these controller methods.
    /// </summary>
    [Authorize(Roles = "Manager")]
    public class ManagementController : Controller
    {
        private readonly IMenuManagerService _menuManagerService;
        private readonly IMenuRetrievalService _menuRetrievalService;
        private readonly ISelectListBuilder _selectListBuilder;
        private readonly IServerManagerService _serverManagerService;

        /// <summary>
        /// Constructs an MVC controller with the required dependencies for menu and server management.
        /// </summary>
        /// <param name="menuManagerService"></param>
        /// <param name="menuRetrievalService"></param>
        /// <param name="selectListBuilder"></param>
        /// <param name="serverManagerService"></param>
        public ManagementController(IMenuManagerService menuManagerService, IMenuRetrievalService menuRetrievalService, ISelectListBuilder selectListBuilder, IServerManagerService serverManagerService)
        {
            _menuManagerService = menuManagerService;
            _menuRetrievalService = menuRetrievalService;
            _selectListBuilder = selectListBuilder;
            _serverManagerService = serverManagerService;
        }

        /// <summary>
        /// Takes the user to the main management web page.
        /// </summary>
        /// <returns>A created ViewResult object.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieves menu items from the service layer. If successful, items are mapped to a model.
        /// </summary>
        /// <returns>A created ViewResult object with a model.</returns>
        [HttpGet]
        public async Task<IActionResult> MenuIndex()
        {
            var model = new List<MenuItem>();

            var result = await _menuRetrievalService.GetAllItemsMVCAsync();

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

        /// <summary>
        /// Two select lists are loaded so that the user can add an item to the menu.
        /// </summary>
        /// <returns>A created ViewResult object with a model.</returns>
        [HttpGet]
        public async Task<IActionResult> AddItem()
        {
            var model = new AddItemForm();

            model.Categories = await _selectListBuilder.BuildCategoriesAsync(TempData);
            model.TimeOfDays = await _selectListBuilder.BuildTimesOfDaysAsync(TempData);

            if (model.Categories == null || model.TimeOfDays == null)
            {
                TempData["Alert"] = Alert.CreateError("An error occurred. Please try again in a few minutes.");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// The model is parsed into an entity and sent to the service layer to add to the database.
        /// The model state is checked beforehand for validation.
        /// </summary>
        /// <param name="model">A model used for adding items to the menu.</param>
        /// <returns>A created ViewResult object with a model.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(AddItemForm model)
        {
            // The select lists must be reloaded before displaying the view.
            model.Categories = await _selectListBuilder.BuildCategoriesAsync(TempData);
            model.TimeOfDays = await _selectListBuilder.BuildTimesOfDaysAsync(TempData);

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();

                var result = await _menuManagerService.AddNewItemAsync(entity);

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

        /// <summary>
        /// Retrieves the associated menu item in accord with the item's ID.
        /// If successful, the rendered view is returned and the user can edit the item.
        /// </summary>
        /// <param name="id">The primary key/ID of the item to edit.</param>
        /// <returns>A created ViewResult object with a model and the retrieved data.</returns>
        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            var result = await _menuRetrievalService.GetItemByIdMVCAsync(id);

            if (result.Ok)
            {
                return View(new EditItemForm(result.Data));
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Maps the model changes to an entity. The entity is then sent to the service layer to be patched.
        /// Model state validation and route matching is checked beforehand.
        /// </summary>
        /// <param name="id">The primary key/ID of the item to edit.</param>
        /// <param name="model">A model used to edit an item's data.</param>
        /// <returns>A created ViewResult object with a model.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(int id, EditItemForm model)
        {
            if (id != model.ItemID)
            {
                TempData["Alert"] = Alert.CreateError("Route mismatch");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var item = model.ToEntity();

                var result = await _menuManagerService.UpdateItemAsync(item);

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

        /// <summary>
        /// Retrieves the current café servers from the database.
        /// If successful, the list of servers is mapped to a model.
        /// </summary>
        /// <returns>A created ViewResult object with the model.</returns>
        [HttpGet]
        public async Task<IActionResult> ServerIndex()
        {
            var model = new ServerList();

            var result = await _serverManagerService.GetAllServersAsync();

            if (result.Ok)
            {
                model.Servers = result.Data;
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// Returns a rendered view with a ServerForm model.
        /// </summary>
        /// <returns>A created ViewResult object with a model.</returns>
        [HttpGet]
        public IActionResult AddServer()
        {
            return View(new ServerForm());
        }

        /// <summary>
        /// Maps the model data to a new Server entity and sends it to the service layer to post it to the database.
        /// The model's state is checked for validation beforehand.
        /// </summary>
        /// <param name="model">A model used to add a new Server to the database.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddServer(ServerForm model)
        {
            if (ModelState.IsValid)
            {
                model.HireDate = DateTime.Now;

                var entity = model.ToEntity();

                var result = await _serverManagerService.AddServerAsync(entity);

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

        /// <summary>
        /// Retrieves the associated Server entity in accord with an ID.
        /// If successful, a rendered view is returned with the server's data mapped to a model.
        /// </summary>
        /// <param name="id">The ID of the server to edit.</param>
        /// <returns>A created ViewResult object with a ServerForm model.</returns>
        [HttpGet]
        public async Task<IActionResult> EditServer(int id)
        {
            var result = await _serverManagerService.GetServerByIdAsync(id);

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

        /// <summary>
        /// Maps the model state to a new object and sends it to the service layer to update the record in the database.
        /// </summary>
        /// <param name="id">The ID of the server to edit.</param>
        /// <param name="model">A model with the server's updated data.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditServer(int id, ServerForm model)
        {
            if (id != model.ServerID)
            {
                TempData["Alert"] = Alert.CreateError("Route mismatch");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();

                var result = await _serverManagerService.UpdateServerAsync(entity);

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