using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.API.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSU.SPORTIDY.API.Payloads.Request.PayOSRequest;
using FSU.SPORTIDY.Service.Services.PaymentServices;
using FSU.SPORTIDY.Service.Interfaces;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayOSService _payOSService;

        public PaymentController(IPayOSService payOSService)
        {
            _payOSService = payOSService;
        }

        [HttpPost(APIRoutes.Payment.createPaymentLink, Name = "createPaymentLink")]
        public async Task<IActionResult> AddAsync([FromBody] PaymentRequest reqObj)
        {
            var paymentReponse = await _payOSService.createPaymentLink((long) reqObj.orderCode);
            return Ok(paymentReponse);
        }
    }
}
