using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.API.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSU.SPORTIDY.API.Payloads.Request.PayOSRequest;
using FSU.SPORTIDY.Service.Services.PaymentServices;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SmartMenuWithAI.API.Payloads.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FSU.SPORTIDY.Service.Services;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayOSService _payOSService;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPayOSService payOSService, IPaymentService paymentService)
        {
            _payOSService = payOSService;
            _paymentService = paymentService;

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
            //var orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            //long parseOrderCode ;
            long.TryParse(reqObj.bookingCode, out long result);
            var returnUrl = $"payment/payment-success?userId={reqObj.userId}&bookingCode={result}&playfieldId={reqObj.playfieldId}&price={reqObj.amount}";
            var cancelUrl = $"payment/payment-cancel?userId={reqObj.userId}&bookingCode={result}&playfieldId={reqObj.playfieldId}&price={reqObj.amount}";
            var paymentReponse = await _payOSService.createPaymentLink(result, amount:reqObj.amount, returnUrl: returnUrl, cancelUrl: cancelUrl, buyerName: reqObj.buyerName, buyerPhone: reqObj.buyerPhone, fieldName: reqObj.playfieldName, hour: reqObj.hour, description: reqObj.description);
            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Create link payment successfully",
                Data = paymentReponse,
                IsSuccess = true
            });
        }

        [HttpGet(APIRoutes.Payment.getPaymentStatistic, Name = "Payment Statistic")]
        public async Task<IActionResult> StatisticPayment()
        {
            try
            {
                var result = await _paymentService.GetAllPaymentsAsync();
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get Statistic Payment Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not get Statistic Payment Success",
                        IsSuccess = false
                    });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    IsSuccess = false
                });

            }
        }
    }
}
