using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ApiControllers
{
    /// <summary>
    /// Handles requests involving the menu of the cafe
    /// </summary>
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        /// <summary>
        /// Injects a service dependency for the menu
        /// </summary>
        /// <param name="menuService"></param>
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Gets entire menu including pricing information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetMenu()
        {
            var result = _menuService.GetMenu();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound("Menu not found.");
            }
        }

        /// <summary>
        /// Get a menu item by ID, including pricing information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItem(int id)
        {
            var result = await _menuService.GetItem(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound("Item not found.");
            }
        }
    }
}