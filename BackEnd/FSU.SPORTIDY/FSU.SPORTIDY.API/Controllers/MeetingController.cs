using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpPost(APIRoutes.Meeting.Add, Name = "AddMeetingAsync")]
        public async Task<IActionResult> AddAsync([FromForm] AddMeetingRequest reqObj)
        {
            try
            {
                var dto = new MeetingModel();
                dto.MeetingName = reqObj.meetingName;
                dto.Address = reqObj.address;
                dto.StartDate = reqObj.startDate;
                dto.EndDate = reqObj.endDate;
                dto.CancelBefore = reqObj.cancelBefore;
                dto.Note = reqObj.note;
                dto.CancelBefore = reqObj.cancelBefore.Value;
                dto.ClubId = reqObj.clubId;
                dto.IsPublic = reqObj.isPublic;
                dto.TotalMember = reqObj.totalMember;
                var MeetingAdd = await _meetingService.Insert(dto,reqObj.currentIdLogin, reqObj.meetingImage);
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

        [Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpDelete(APIRoutes.Meeting.Delete, Name = "DeleteMeetingAsync")]
        public async Task<IActionResult> DeleteAsynce([FromQuery] int meetingId)
        {
            try
            {
                var result = await _meetingService.Delete(meetingId);
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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpPut(APIRoutes.Meeting.Update, Name = "UpdateMeetingAsync")]
        public async Task<IActionResult> UpdateUserAsync([FromQuery(Name = "meetingId")]int id, [FromForm] UpdateMeetingRequest reqObj)
        {
            try
            {
                var dto = new MeetingModel();
                dto.MeetingName = reqObj.meetingName;
                dto.StartDate = reqObj.startDate;
                dto.EndDate = reqObj.endDate;
                dto.Address = reqObj.address;
                dto.TotalMember = reqObj.totalMember;
                dto.CancelBefore = reqObj.cancelBefore;
                dto.Note = reqObj.note;
                dto.IsPublic = reqObj.isPublic;
                dto.MeetingImage = reqObj.meetingImage;
                var result = await _meetingService.Update(dto);
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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpGet(APIRoutes.Meeting.GetAll, Name = "GetMeetingAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery(Name = "currIdLogin")] int currIdLoginId
           , [FromQuery(Name = "searchKey")] string? searchKey
           , [FromQuery(Name = "pageNumber")] int pageNumber = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "pageSize")] int PageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var allAccount = await _meetingService.Get(currIdLoginId, searchKey!, PageIndex: pageNumber, PageSize: PageSize);

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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpGet(APIRoutes.Meeting.GetByID, Name = "GetMeetingByID")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "meetingId")] int meetingId)
        {
            try
            {
                var user = await _meetingService.GetByID(meetingId);

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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpPut(APIRoutes.Meeting.UpdateRoleInMeeting)]
        public async Task<IActionResult> UpdateRoleInMeeting([FromBody] UpdateRoleInMeetingRequest request)
        {
            try
            {
                var updatedUserMeeting = await _meetingService.UpdateRoleInMeeting(request.userId, request.meetingId, request.roleInMeeting);

                if (updatedUserMeeting == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Meeting or User not found",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Role updated successfully",
                    Data = updatedUserMeeting,
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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpDelete(APIRoutes.Meeting.KickUser)]
        public async Task<IActionResult> KickUser([FromBody] KickUserRequest request)
        {
            try
            {
                var result = await _meetingService.kickUserOfMeeting(request.userId, request.meetingId);

                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "User or Meeting not found",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "User removed from meeting successfully",
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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpGet(APIRoutes.Meeting.getAllMeetingOfUser)]
        public async Task<IActionResult> GetAllUsersInMeeting([FromRoute] int userId)
        {
            try
            {
                var usersInMeeting = await _meetingService.GetAllMeetingByUserID(userId);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Users fetched successfully",
                    Data = usersInMeeting,
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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpGet(APIRoutes.Meeting.getAllUserInMeeting)]
        public async Task<IActionResult> GetAllMeetingOfUser([FromRoute] int meetingId)
        {
            try
            {
                var usersInMeeting = await _meetingService.getUsersInMeeting(meetingId);

                if (usersInMeeting == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Meeting not found or no users in meeting",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Users fetched successfully",
                    Data = usersInMeeting,
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

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpPost(APIRoutes.Meeting.EngageToMeeting)]
        public async Task<IActionResult> InsertUserMeeting([FromBody] InsertUserMeetingRequest request)
        {
            try
            {
                var newUserMeeting = await _meetingService.insertUserMeeting(request.userId, request.meetingId);

                if (newUserMeeting == null)
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Failed to add user to meeting",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "User added to meeting successfully",
                    Data = newUserMeeting,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message.ToString(),
                    Data = null,
                    IsSuccess = false
                });
            }
        }

    }
}
