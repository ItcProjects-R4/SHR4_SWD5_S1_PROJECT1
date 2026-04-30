using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymobService _paymobService;

        public PaymentController(IPaymobService paymobService)
        {
            _paymobService = paymobService;
        }

        [HttpPost("pay/{orderId}")]
        public async Task<IActionResult> Pay(int orderId)
        {
            // هنجيب الأوردر من الـ DB
            // مؤقتاً هنحط قيم ثابتة للتجربة
            int amountCents = 15000; // 150 جنيه = 15000 cents
            string customerEmail = "test@test.com";

            var paymentUrl = await _paymobService.GetPaymentUrlAsync(orderId, amountCents, customerEmail);

            return Ok(new { paymentUrl });
        }
    }
}
