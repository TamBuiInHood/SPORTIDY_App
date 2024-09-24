using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.API.Payloads.Request.MeetingRequest;
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

        [HttpPost(APIRoutes.Comment.Add)]
        public async Task<IActionResult> Insert([FromBody] AddCommentRequest reqObj)
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
    }

}
