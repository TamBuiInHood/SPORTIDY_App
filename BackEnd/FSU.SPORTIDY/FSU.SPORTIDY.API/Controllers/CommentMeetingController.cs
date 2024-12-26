using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CommentMeetingController : ControllerBase
    {
        private readonly ICommentInMeetingService _commentService;
        private readonly WebSocketService _websocketService;

        public CommentMeetingController(ICommentInMeetingService commentService, WebSocketService websocketService)
        {
            _commentService = commentService;
            _websocketService = websocketService;
        }

        [HttpPost(APIRoutes.Comment.Add)]
        public async Task<IActionResult> Insert([FromForm] AddCommentRequest reqObj)
        {
            try
            {

                var result = await _commentService.Insert(reqObj.content!, reqObj.meetingId, reqObj.userId, reqObj.image!);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    IsSuccess = true,
                    Message = "comment successfully",
                    Data = result
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = true,
                    Message = "comment has error",
                    Data = ex.ToString()
                });
            }

        }

        // WebSocket connection
        [HttpGet(APIRoutes.WebSocket.ws)]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _websocketService.AddClient(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        [HttpGet(APIRoutes.Comment.GetAll)]
        public async Task<IActionResult> GetCommentsByMeeting([FromRoute(Name = "meetingId")] int meetingId,
                                                                int pageSize = Page.DEFAULT_PAGE_SIZE,
                                                                int pageIndex = Page.DEFAULT_PAGE_INDEX)
        {
            try
            {
                var paginatedComments = await _commentService.GetByMeetingId(meetingId, pageSize, pageIndex);

                //if (paginatedComments == null || !paginatedComments.List.Any())
                //{
                //    return NotFound(new BaseResponse
                //    {
                //        StatusCode = StatusCodes.Status404NotFound,
                //        Message = "No comments found for the specified meeting.",
                //        IsSuccess = false
                //    });
                //}

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Comments retrieved successfully.",
                    Data = paginatedComments,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpDelete(APIRoutes.Comment.Delete)]
        public async Task<IActionResult> DeleteComment([FromRoute(Name = "commentId")] int commentId)
        {
            try
            {
                var result = await _commentService.Delete(commentId);

                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Comment not found or deletion failed.",
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Comment deleted successfully.",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpGet(APIRoutes.Comment.GetByID)]
        public async Task<IActionResult> GetCommentById([FromRoute(Name = "commentId")] int commentId)
        {
            try
            {
                var comment = await _commentService.GetById(commentId);

                if (comment == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Comment not found.",
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Comment retrieved successfully.",
                    Data = comment,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpPut(APIRoutes.Comment.Update)]
        public async Task<IActionResult> UpdateComment([FromForm]UpdateCommentRequest reqObj)
        {
            try
            {
                var updatedComment = await _commentService.Update(reqObj.content, reqObj.commentId, reqObj.image);

                if (updatedComment == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Comment not found or update failed.",
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Comment updated successfully.",
                    Data = updatedComment,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }


    }

}
