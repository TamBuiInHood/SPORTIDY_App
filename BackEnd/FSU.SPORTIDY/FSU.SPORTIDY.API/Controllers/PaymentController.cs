using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.API.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSU.SPORTIDY.API.Payloads.Request.PayOSRequest;
using FSU.SPORTIDY.Service.Services.PaymentServices;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SmartMenuWithAI.API.Payloads.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Some thing are need"
                }) ;
            }
            var orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            var returnUrl = $"payment/payment-success/userid={reqObj.userId}&order-code={orderCode}";
            var cancelUrl = $"payment/payment-cancel/userid={reqObj.userId}&order-code={orderCode}";
            var paymentReponse = await _payOSService.createPaymentLink(orderCode, amount:reqObj.amount, returnUrl: returnUrl, cancelUrl: cancelUrl, buyerName: reqObj.buyerName, buyerPhone: reqObj.buyerPhone, fieldName: reqObj.playfieldName, hour: reqObj.hour, description: reqObj.description);
            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Create link payment successfully",
                Data = paymentReponse,
                IsSuccess = true
            });
        }
    }
}
