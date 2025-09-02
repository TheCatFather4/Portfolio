using Cafe.Core.DTOs;
using Cafe.Core.Entities;
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
        /// Retrieves a customer's shopping bag and the items inside it
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(ShoppingBag), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShoppingBag(int customerId)
        {
            var result = await _shoppingBagService.GetShoppingBagAsync(customerId);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        /// <summary>
        /// Adds an item to customer's shopping bag
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="itemDto"></param>
        /// <returns></returns>
        [HttpPost("{customerId}/items")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItemToBag(int customerId, [FromBody] AddItem itemDto)
        {
            var result = await _shoppingBagService.AddItemToBagAsync(customerId, itemDto.ItemId, itemDto.Quantity);

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
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut("{customerId}/items/{shoppingBagItemId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateItemQuantity(int customerId, int shoppingBagItemId, [FromBody] UpdateQuantity quantity)
        {
            var result = await _shoppingBagService.UpdateItemQuantityAsync(customerId, shoppingBagItemId, quantity.NewQuantity);

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
