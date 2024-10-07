using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSU.SPORTIDY.Service.BusinessModel.FriendShipBSModels;
using Microsoft.AspNetCore.Authorization;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.API.Payloads.Request.FriendShipRequest;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly FriendShipService _friendshipService;

        public FriendshipController(FriendShipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        //[Authorize(Roles = UserRoleConst.PLAYER)]
        [HttpPost(APIRoutes.Friendship.Add, Name = "AddFriendshipAsync")]
        public async Task<IActionResult> AddAsync([FromBody] AddFriendRequest reqObj)
        {
            try
            {
                var addFriend = await _friendshipService.Insert(currentLoginID: reqObj.currentIdLogin, UserID2: reqObj.userId2);
                if (addFriend == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "add friend fail.",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "add friend Successfully",
                    Data = addFriend,
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
        [HttpDelete(APIRoutes.Friendship.Delete, Name = "DeleteFriendshipAsync")]
        public async Task<IActionResult> DeleteAsynce([FromQuery] int frinedshipId)
        {
            try
            {
                var result = await _friendshipService.Delete(frinedshipId);
                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Friend not found",
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
        [HttpPut(APIRoutes.Friendship.Update, Name = "UpdateFrienshipAsync")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateFriendShipRequest reqObj)
        {
            try
            {
                var result = await _friendshipService.updateStatus(reqObj.currentIdLogin, reqObj.userId2, (int)reqObj.status);
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
        [HttpGet(APIRoutes.Friendship.GetAll, Name = "GetFriendshipAsync")]
        public async Task<IActionResult> GetAllAsync([FromQuery(Name = "currIdLogin")] int currIdLoginID
           , [FromQuery(Name = "searchKey")] string? searchKey
           , [FromQuery(Name = "pageNumber")] int pageNumber = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "pageSize")] int PageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var allAccount = await _friendshipService.Get(currIdLoginID, searchKey!, PageIndex: pageNumber, PageSize: PageSize);

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
        [HttpGet(APIRoutes.Friendship.GetBy2UserId, Name = "GetBy2UserId")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "userId1")] int userId1, [FromRoute(Name = "userid2")] int userId2)
        {
            try
            {
                var user = await _friendshipService.GetBy2ID(userId1, userId2);

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

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet(APIRoutes.Friendship.GetByID, Name = "GetFriendById")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "friendshipId")] int friendshipId)
        {
            try
            {
                var user = await _friendshipService.GetByID(friendshipId);

                if (user == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Friend not found",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get friend successfully",
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

