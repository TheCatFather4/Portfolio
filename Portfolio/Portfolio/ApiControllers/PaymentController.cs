using Cafe.Core.DTOs.Requests;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ApiControllers
{
    /// <summary>
    /// Handles requests concerning payments. Authentication required.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Constructs a controller with the required dependency for processing payments.
        /// </summary>
        /// <param name="paymentService">A dependency used to access and create payments.</param>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Receives a payment request from the client and creates a new payment record.
        /// </summary>
        /// <param name="dto">A DTO with the data required to make a payment.</param>
        /// <response code="200">Payment confirmation data.</response>
        /// <response code="400">Data not valid.</response>
        /// <response code="401">Not authorized.</response>
        /// <response code="500">Server error.</response>
        [HttpPost("process")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _paymentService.ProcessPaymentAsync(dto);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            return StatusCode(500, result.Message);
        }
    }
}