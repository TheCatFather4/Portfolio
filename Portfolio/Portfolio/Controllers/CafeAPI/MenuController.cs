using Cafe.Core.DTOs.Responses;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers.ApiControllers
{
    /// <summary>
    /// Handles requests concerning the menu of the café.
    /// </summary>
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRetrievalService _menuRetrievalService;

        /// <summary>
        /// Constructs a controller with the required dependency to retrieve item and price data. 
        /// </summary>
        /// <param name="menuService">A dependency used for menu retrieval.</param>
        public MenuController(IMenuRetrievalService menuService)
        {
            _menuRetrievalService = menuService;
        }

        /// <summary>
        /// Retrieves the entire menu of item and price data.
        /// </summary>
        /// <response code="200">Returns the contents of the menu.</response>
        /// <response code="500">Server side error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenu()
        {
            var result = await _menuRetrievalService.GetAllItemsAPIAsync();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }

        /// <summary>
        /// Retrieves an item and its pricing data.
        /// </summary>
        /// <param name="itemId">The identifier used to retrieve a specific item's data.</param>
        /// <response code="200">Returns the item and its pricing data.</response>
        /// <response code="404">Item was not found.</response>
        /// <response code="500">Server side error.</response>
        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItem(int itemId)
        {
            var result = await _menuRetrievalService.GetItemByIdAPIAsync(itemId);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else
            {
                if (result.Data == null)
                {
                    return NotFound(result.Message);
                }

                return StatusCode(500, result.Message);
            }
        }
    }
}