using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ApiControllers
{
    /// <summary>
    /// Handles requests involving the customer's shopping bag
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class ShoppingBagController : ControllerBase
    {
        private readonly IShoppingBagService _shoppingBagService;

        /// <summary>
        /// Injects a service dependency for the shopping bag
        /// </summary>
        /// <param name="shoppingBagService"></param>
        public ShoppingBagController(IShoppingBagService shoppingBagService)
        {
            _shoppingBagService = shoppingBagService;
        }

        /// <summary>
        /// Retrieves a customer's shopping bag and any items inside it
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(ShoppingBagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetShoppingBag(int customerId)
        {
            var result = await _shoppingBagService.GetShoppingBagByCustomerIdAsync(customerId);

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
        /// Adds an item to a customer's shopping bag
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{customerId}/items")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddItemToBag(int customerId, [FromBody] AddItemRequest dto)
        {
            var result = await _shoppingBagService.AddItemToShoppingBagAsync(dto);

            if (result.Ok)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Removes an item from a customer's shopping bag
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="shoppingBagItemId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}/items/{shoppingBagItemId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveItemFromBag(int customerId, int shoppingBagItemId)
        {
            var result = await _shoppingBagService.RemoveItemFromBagAsync(customerId, shoppingBagItemId);

            if (result.Ok)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Updates an item's quantity in customer's shopping bag
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="shoppingBagItemId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{customerId}/items/{shoppingBagItemId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateItemQuantity(int customerId, int shoppingBagItemId, [FromBody] UpdateQuantityRequest dto)
        {
            var result = await _shoppingBagService.UpdateItemQuantityAsync(customerId, shoppingBagItemId, dto.Quantity);

            if (result.Ok)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Empties a customer's shopping bag
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ClearShoppingBag(int customerId)
        {
            var result = await _shoppingBagService.ClearShoppingBagAsync(customerId);

            if (result.Ok)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}