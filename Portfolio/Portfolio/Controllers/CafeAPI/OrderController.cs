using Cafe.Core.DTOs.Requests;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers.ApiControllers
{
    /// <summary>
    /// Handles requests concerning orders. Authentication required.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Constructs a controller with the required dependency to place orders and access order history.
        /// </summary>
        /// <param name="orderService">A dependency used in accessing data concerning orders.</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Creates a new order for a customer.
        /// </summary>
        /// <param name="dto">A DTO with the data required to create a new order.</param>
        /// <response code="201">The data from the newly created order.</response>
        /// <response code="400">Client data not valid.</response>
        /// <response code="401">Not authorized.</response>
        /// <response code="500">Server error.</response>
        [HttpPost("new")]
        [ProducesResponseType(typeof(CafeOrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] CafeOrderRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.CreateNewOrderAsync(dto);

            if (result.Ok)
            {
                return Created(string.Empty, result.Data);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }

        /// <summary>
        /// Retrieves the details of a customer's order.
        /// </summary>
        /// <param name="customerId">An ID used to identify a specific customer.</param>
        /// <param name="orderId">An ID used to identity a specific order.</param>
        /// <response code="200">Customer's order details.</response>
        /// <response code="401">Not authorized.</response>
        /// <response code="404">Order not found.</response>
        [HttpGet("customer/{customerId}/{orderId}")]
        [ProducesResponseType(typeof(CafeOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetails(int customerId, int orderId)
        {
            var result = await _orderService.GetOrderDetailsAsync(orderId);

            if (result.Ok && result.Data != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        /// <summary>
        /// Retrieves a customer's entire order history.
        /// </summary>
        /// <param name="customerId">An ID used to identify a specific customer.</param>
        /// <response code="200">Customer's order history.</response>
        /// <response code="401">Not authorized.</response>
        /// <response code="404">Order history not found.</response>
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(List<CafeOrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderHistory(int customerId)
        {
            var result = await _orderService.GetOrderHistoryAsync(customerId);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}