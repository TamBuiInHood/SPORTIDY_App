﻿using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSU.SPORTIDY.API.Payloads.Request.BookingRequest;
using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using FSU.SPORTIDY.Common.Status;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost(APIRoutes.Booking.Add, Name = "AddBookingAsync")]
        public async Task<IActionResult> AddAsync([FromForm] AddBookingRequest reqObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Create booking fail.",
                        Data = ModelState,
                        IsSuccess = false
                    });
                }
                var dto = new BookingModel();
                dto.BookingCode = reqObj.bookingCode;
                dto.DateStart  = reqObj.dateStart;
                dto.DateEnd = reqObj.dateEnd;
                dto.Price = reqObj.price;
                dto.PlayFieldId = reqObj.playFieldId;
                dto.Description = reqObj.description;
                dto.CustomerId = reqObj.customerId;

                var MeetingAdd = await _bookingService.Insert(dto, reqObj.barCode!);
                if (MeetingAdd == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Create booking fail.",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Creat Booking Successfully",
                    Data = MeetingAdd,
                    IsSuccess = true
                });


            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpDelete(APIRoutes.Booking.Delete, Name = "DeleteBookingAsync")]
        public async Task<IActionResult> DeleteAsynce([FromRoute(Name = "bookingId")] int bookingId)
        {
            try
            {
                var result = await _bookingService.Delete(bookingId);
                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Booking not found",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Delete successfully",
                    Data = null,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut(APIRoutes.Booking.Update, Name = "UpdateBookingAsync")]
        public async Task<IActionResult> UpdateBookingAsync([FromForm] UpdateBookingRequest reqObj)
        {
            try
            {
                var dto = new BookingModel();
                dto.BookingId = reqObj.bookingId;
                dto.DateStart = reqObj.dateStart;
                dto.DateEnd = reqObj.dateEnd;
                dto.Description = reqObj.description;
                dto.CustomerId = reqObj.customerId;
                var result = await _bookingService.Update(dto, reqObj.barCode);
                if (result == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Update fail",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Update successfully",
                    Data = result,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut(APIRoutes.Booking.UpdateStatus, Name = "UpdateBookingStatusAsync")]
        public async Task<IActionResult> UpdateStatusAsync([FromForm] UpdateBookingStatusRequest reqObj)
        {
            try
            {
                var result = await _bookingService.UpdateStatus(reqObj.bookingId,(int) reqObj.status);
                if (result == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Update fail",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Update successfully",
                    Data = result,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet(APIRoutes.Booking.GetByUserID, Name = "GetBookingByUserIDAsync")]
        public async Task<IActionResult> GetByUserIDAsync([FromQuery(Name = "currIdLogin")] int currIdLoginID
           , [FromQuery(Name = "searchKey")] string? searchKey
           , [FromQuery(Name = "pageNumber")] int pageNumber = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "pageSize")] int PageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var allAccount = await _bookingService.GetByUserId(currIdLoginID, searchKey!, PageIndex: pageNumber, PageSize: PageSize);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Load successfully",
                    Data = allAccount,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet(APIRoutes.Booking.GetAll, Name = "GetBookingAsync")]
        public async Task<IActionResult> GetAllAsync( [FromQuery(Name = "searchKey")] string? searchKey
           , [FromQuery(Name = "pageNumber")] int pageNumber = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "pageSize")] int PageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var allAccount = await _bookingService.GetAll( searchKey!, PageIndex: pageNumber, PageSize: PageSize);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Load successfully",
                    Data = allAccount,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet(APIRoutes.Booking.GetByID, Name = "GetBookingByID")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "bookingId")] int bookingId)
        {
            try
            {
                var user = await _bookingService.GetById(bookingId);

                if (user == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Booking not found",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get Booking successfully",
                    Data = user,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null,
                    IsSuccess = false
                });
            }
        }

        [HttpGet(APIRoutes.Booking.RevenuePlayField, Name = "Revenue PlayField By Id And Year")]
        public async Task<IActionResult> GetPlayFieldRevenue([FromRoute(Name = "playFieldId")]int playFieldId, 
                                                                [FromRoute(Name = "year")] int year)
        {
            var revenueResponse = await _bookingService.GetPlayFieldRevenueAsync(playFieldId, year);

            if (revenueResponse == null)
            {
                return NotFound("PlayField not found");
            }

            return Ok(revenueResponse);
        }

        [HttpGet(APIRoutes.Booking.RevenuePlayFieldForAdmin, Name = "Revenue PlayField For Admin")]
        public async Task<IActionResult> GetPlayFieldRevenue([FromRoute(Name = "year")] int year)
        {
            var revenueResponse = await _bookingService.GetAnnualRevenueForAdminAsync(year);

            if (revenueResponse == null)
            {
                return NotFound("No bookings found for this year.");
            }

            return Ok(revenueResponse);
        }

        [HttpGet(APIRoutes.Booking.StatisticPlayFieldTypePercentage, Name = "Statistic PlayField Type Percentage")]
        public async Task<IActionResult> GetFieldTypePercentage([FromRoute(Name = "year")] int year)
        {
            var result = await _bookingService.GetFieldTypePercentageAsync(year);

            if (result == null)
            {
                return NotFound("No bookings found.");
            }

            return Ok(result);
        }

    }
}
