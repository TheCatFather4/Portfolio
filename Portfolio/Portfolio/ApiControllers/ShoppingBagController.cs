using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ApiControllers
{
    /// <summary>
    /// Handles requests concerning the customer's shopping bag.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class ShoppingBagController : ControllerBase
    {
        private readonly IShoppingBagService _shoppingBagService;

        /// <summary>
        /// Constructs a controller with the dependency required to invoke shopping bag service members.
        /// </summary>
        /// <param name="shoppingBagService">A dependency used to access a customer's shopping bag data.</param>
        public ShoppingBagController(IShoppingBagService shoppingBagService)
        {
            _shoppingBagService = shoppingBagService;
        }

        /// <summary>
        /// Adds an item to a customer's shopping bag.
        /// </summary>
        /// <param name="customerId">A CustomerID that identifies which shopping bag to use.</param>
        /// <param name="dto">A DTO containing the data of the item to add.</param>
        /// <returns>A status code.</returns>
        [HttpPost("{customerId}/add")]
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
        /// Empties a customer's shopping bag.
        /// </summary>
        /// <param name="customerId">A CustomerID that identifies which shopping bag to use.</param>
        /// <returns>A status code.</returns>
        [HttpDelete("{customerId}/empty")]
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

        /// <summary>
        /// Retrieves a customer's shopping bag and its contents.
        /// </summary>
        /// <param name="customerId">A CustomerID that identifies which shopping bag to use.</param>
        /// <returns>A status code. If successful, a response DTO is returned with the customer's shopping bag data.</returns>
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
        /// Removes an item from a customer's shopping bag.
        /// </summary>
        /// <param name="customerId">A CustomerID that identifies which shopping bag to use.</param>
        /// <param name="shoppingBagItemId">A ShoppingBagItemID that identifies which item to remove.</param>
        /// <returns>A status code.</returns>
        [HttpDelete("{customerId}/remove/{shoppingBagItemId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveItemFromBag(int customerId, int shoppingBagItemId)
        {
            var result = await _shoppingBagService.RemoveItemFromShoppingBagAsync(customerId, shoppingBagItemId);

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
        /// Updates the quantity of an item in the customer's shopping bag.
        /// </summary>
        /// <param name="customerId">A CustomerID that identifies which shopping bag to use.</param>
        /// <param name="shoppingBagItemId">A ShoppingBagItemID that identifies which item to update.</param>
        /// <param name="dto">The new quantity value</param>
        /// <returns></returns>
        [HttpPut("{customerId}/update/{shoppingBagItemId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateItemQuantity(int customerId, int shoppingBagItemId, [FromBody] UpdateQuantityRequest dto)
        {
            var result = await _shoppingBagService.UpdateItemQuantityAsync(shoppingBagItemId, dto.Quantity);

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