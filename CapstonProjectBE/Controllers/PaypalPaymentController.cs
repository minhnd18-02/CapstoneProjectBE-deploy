using Application.IService;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalPaymentController : ControllerBase
    {
        private readonly IPaypalPaymentService _paypalPaymentService;
        private readonly IAuthenService _authenService;
        public PaypalPaymentController(IPaypalPaymentService paypalPaymentService, IAuthenService authenService)
        {
            _paypalPaymentService = paypalPaymentService;
            _authenService = authenService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(int projectId, decimal amount)
        {
            //var user = await _authenService.GetUserByTokenAsync(HttpContext.User);
            var result = await _paypalPaymentService.CreatePaymentAsync(projectId, amount, "http://localhost:50875/payment", "http://localhost:50875/user/cart");

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("execute")]
        [AllowAnonymous]
        public async Task<IActionResult> ExecutePayment([FromQuery] string paymentId, [FromQuery] string token, [FromQuery] string PayerID)
        {
            var result = await _paypalPaymentService.ExecutePaymentAsync(paymentId, PayerID);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
