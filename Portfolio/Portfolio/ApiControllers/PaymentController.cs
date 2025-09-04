using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ApiControllers
{
    /// <summary>
    /// Handles requests involving payments
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Injects a service dependency
        /// </summary>
        /// <param name="paymentService"></param>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Processes payments and returns a confirmation status
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("process")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public IActionResult ProcessPayment([FromBody] PaymentRequest dto)
        {
            var result = _paymentService.ProcessPayment(dto);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
