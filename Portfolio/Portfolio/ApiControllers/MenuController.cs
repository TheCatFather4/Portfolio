using Cafe.Core.DTOs;
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
        /// Retrieves the entire menu including pricing information for each item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        public IActionResult GetMenu()
        {
            var result = _menuService.GetMenuAPI();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else if (result.Data == null)
            {
                return StatusCode(503, result.Message);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }

        /// <summary>
        /// Retrieves an individual item including its pricing information
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetItem(int itemId)
        {
            var result = await _menuService.GetItemAPIAsync(itemId);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else if (result.Data == null)
            {
                return StatusCode(503, result.Message);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }
    }
}