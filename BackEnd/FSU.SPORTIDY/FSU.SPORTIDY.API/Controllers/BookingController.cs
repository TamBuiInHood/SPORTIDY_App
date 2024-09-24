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
                dto.BookingCode = reqObj.BookingCode;
                dto.DateStart  = reqObj.DateStart;
                dto.DateEnd = reqObj.DateEnd;
                dto.Price = reqObj.Price;
                dto.PlayFieldId = reqObj.PlayFieldId;
                dto.Description = reqObj.Description;
                dto.CustomerId = reqObj.CustomerId;

                var MeetingAdd = await _bookingService.Insert(dto, reqObj.BarCode!);
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
        public async Task<IActionResult> DeleteAsynce([FromQuery(Name = "booking-id")] int bookingId)
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
                dto.BookingId = reqObj.BookingId;
                dto.DateStart = reqObj.DateStart;
                dto.DateEnd = reqObj.DateEnd;
                dto.Description = reqObj.Description;
                dto.CustomerId = reqObj.CustomerId;
                var result = await _bookingService.Update(dto, reqObj.BarCode);
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
                var result = await _bookingService.UpdateStatus(reqObj.BookingId,(int) reqObj.Status);
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
        public async Task<IActionResult> GetByUserIDAsync([FromQuery(Name = "curr-id-login")] int currIdLoginID
           , [FromQuery(Name = "search-key")] string? searchKey
           , [FromQuery(Name = "page-number")] int pageNumber = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "page-size")] int PageSize = Page.DEFAULT_PAGE_SIZE)
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
        public async Task<IActionResult> GetAllAsync( [FromQuery(Name = "search-key")] string? searchKey
           , [FromQuery(Name = "page-number")] int pageNumber = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "page-size")] int PageSize = Page.DEFAULT_PAGE_SIZE)
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
        public async Task<IActionResult> GetAsync([FromRoute(Name = "booking-id")] int bookingId)
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
    }
}