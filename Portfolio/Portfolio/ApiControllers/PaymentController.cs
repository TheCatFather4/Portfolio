using Cafe.Core.DTOs;
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
        /// Processes a payment.
        /// </summary>
        /// <param name="dto">A DTO with the data required to make a payment.</param>
        /// <response code="200">Payment confirmation data.</response>
        /// <response code="400">Data not valid.</response>
        /// <response code="401">Not authorized.</response>
        [HttpPost("process")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest dto)
        {
            var result = await _paymentService.ProcessPaymentAsync(dto);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}