using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Common.Constants;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSU.SPORTIDY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingSerivce;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingSerivce = meetingService;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost(APIRoutes.Meeting.Add, Name = "AddMeetingAsync")]
        public async Task<IActionResult> AddAsync([FromBody] AddMeetingRequest reqObj)
        {
            try
            {
                var dto = new Service.BusinessModel.MeetingModels.MeetingDTO();
                dto.MeetingName = reqObj.MeetingName;
                dto.Address = reqObj.Address;
                dto.MeetingImage = reqObj.MeetingImage;
                dto.StartDate = reqObj.StartDate;
                dto.EndDate = reqObj.EndDate;
                dto.CancelBefore = reqObj.CancelBefore;
                dto.Note = reqObj.Note;
                dto.CancelBefore = reqObj.CancelBefore.Value;
                dto.ClubId = reqObj.ClubId;
                dto.IsPublic = reqObj.IsPublic;
                dto.TotalMember = reqObj.TotalMember;
                var MeetingAdd = await _meetingSerivce.Insert(dto,reqObj.InvitedFriend,reqObj.currentIDLogin);
                if (MeetingAdd == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Create Meeting fail.",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Creat Meeting Successfully",
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
        [HttpDelete(APIRoutes.Meeting.Delete, Name = "DeleteMeetingAsync")]
        public async Task<IActionResult> DeleteAsynce([FromQuery] int meetingId)
        {
            try
            {
                var result = await _meetingSerivce.Delete(meetingId);
                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Meeting not found",
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
        [HttpPut(APIRoutes.Meeting.Update, Name = "UpdateMeetingAsync")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateMeetingRequest reqObj)
        {
            try
            {
                var dto = new MeetingDTO();
                dto.MeetingName = reqObj.MeetingName;
                dto.StartDate = reqObj.StartDate;
                dto.EndDate = reqObj.EndDate;
                dto.Address = reqObj.Address;
                dto.TotalMember = reqObj.TotalMember;
                dto.CancelBefore = reqObj.CancelBefore;
                dto.Note = reqObj.Note;
                dto.IsPublic = reqObj.IsPublic;
                dto.MeetingImage = reqObj.MeetingImage;
                var result = await _meetingSerivce.Update(dto);
                if (result != null)
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
        [HttpGet(APIRoutes.Meeting.GetAll, Name = "GetMeetingAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery(Name = "curr-id-login")] int currIdLoginID
           , [FromQuery(Name = "search-key")] string? searchKey
           , [FromQuery(Name = "page-number")] int pageNumber = Page.DefaultPageIndex
           , [FromQuery(Name = "page-size")] int PageSize = Page.DefaultPageSize)
        {
            try
            {
                var allAccount = await _meetingSerivce.Get(currIdLoginID, searchKey!, PageIndex: pageNumber, PageSize: PageSize);

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
        [HttpGet(APIRoutes.Meeting.GetByID, Name = "GetMeetingByID")]
        public async Task<IActionResult> GetAsync([FromQuery] int Id)
        {
            try
            {
                var user = await _meetingSerivce.GetByID(Id);

                if (user == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Meeting not found",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get meeting successfully",
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
