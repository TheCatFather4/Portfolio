using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ApiControllers
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
        /// Constructs a controller with the required dependency to retrieve all Item records. 
        /// </summary>
        /// <param name="menuService">A dependency used for menu retrieval.</param>
        public MenuController(IMenuRetrievalService menuService)
        {
            _menuRetrievalService = menuService;
        }

        /// <summary>
        /// Retrieves the entire menu of Item records and ItemPrice records. 
        /// The data returned is a list of ItemResponse DTOs.
        /// </summary>
        /// <returns>A list of ItemResponse DTOs.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetMenu()
        {
            var result = await _menuRetrievalService.GetAllItemsAPIAsync();

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
        /// Retrieves an Item and ItemPrice record based on an ItemID.
        /// The data is returned as an ItemResponse DTO.
        /// </summary>
        /// <param name="itemId">An ItemID used to retrieve an Item record.</param>
        /// <returns>An ItemResponse DTO.</returns>
        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetItem(int itemId)
        {
            var result = await _menuRetrievalService.GetItemByIdAPIAsync(itemId);

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