using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Portfolio.ApiControllers
{
    /// <summary>
    /// Handles requests involving placing orders
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        /// <summary>
        /// Injects an order service and a logger
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="logger"></param>
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new order for a customer. Items are removed from the shopping bag and moved to the order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CafeOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateOrder request.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation($"Attempting to create order for Customer ID: {request.CustomerId}");
            var result = await _orderService.CreateOrderAsync(request.CustomerId, request.PaymentTypeId, request.Tip);

            if (result.Ok)
            {
                _logger.LogInformation($"Order {result.Data?.OrderID} created successfully for customer {request.CustomerId}");
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }
            else
            {
                _logger.LogError($"Failed to create order for customer {request.CustomerId}: {result.Message}");
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Retrieves the details of an order including its item data
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(CafeOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            _logger.LogInformation($"Attempting to retrieve details for order ID: {orderId}");
            var result = await _orderService.GetOrderDetailsAsync(orderId);

            if (result.Ok && result.Data != null)
            {
                return Ok(result.Data);
            }
            else
            {
                _logger.LogWarning($"Order with ID {orderId} not found or an error occurred: {result.Message}");
                return NotFound(result.Message);
            }
        }

        /// <summary>
        /// Retrieves a customer's order history, including items associated with each order
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(List<CafeOrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderHistory(int customerId)
        {
            _logger.LogInformation($"Attempting to retrieve orders for customer ID: {customerId}");
            var result = await _orderService.GetOrdersAsync(customerId);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            else
            {
                _logger.LogWarning($"No orders found for customer ID {customerId} or an error occurred: {result.Message}");
                return BadRequest(result.Message);
            }
        }
    }
}
