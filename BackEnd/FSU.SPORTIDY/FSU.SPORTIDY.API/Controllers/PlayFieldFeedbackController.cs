using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldFeedbackModels;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSU.SPORTIDY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayFieldFeedbackController : ControllerBase
    {
        public readonly IPlayFieldFeedbackService _playFieldFeedbackService;

        public PlayFieldFeedbackController(IPlayFieldFeedbackService playFieldFeedbackService)
        {
            _playFieldFeedbackService = playFieldFeedbackService;
        }

        [HttpGet(APIRoutes.PlayFieldFeedback.GetAll, Name = "Get All PlayFieldFeedback")]
        public async Task<IActionResult> GetAllPlayFieldFeedback(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _playFieldFeedbackService.GetAllPlayFieldFeedback(paginationParameter);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get all playfield feedback success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not get any playfield feedback",
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

        [HttpGet(APIRoutes.PlayFieldFeedback.GetByID, Name = "Get PlayFieldFeedback by PlayFieldID")]
        public async Task<IActionResult> GetPlayFieldFeedbackByPlayFieldId([FromRoute] int feedbackId)
        {
            try
            {
                var result = await _playFieldFeedbackService.GetPlayFieldFeedbackById(feedbackId);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get playfield feedback by playfield Id success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not get playfield feedback with that id",
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

        [HttpGet(APIRoutes.PlayFieldFeedback.GetByOwnerId, Name = "Get PlayFieldFeedback by OwnerId")]
        public async Task<IActionResult> GetPlayFieldFeedbackByOwnerId([FromRoute] int ownerId)
        {
            try
            {
                var result = await _playFieldFeedbackService.GetPlayFieldFeedbackByOwnerId(ownerId);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get playfield feedback by owner id success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not get any playfield feedback with that owner id",
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

        [HttpPost(APIRoutes.PlayFieldFeedback.Create, Name ="Create PlayField Feedback")]
        public async Task<IActionResult> CreatePlayFieldFeedback([FromBody] CreatePlayFieldFeedbackModel createFeedbackModel)
        {
            try
            {
                var result = await _playFieldFeedbackService.CreateFeedback(createFeedbackModel);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Create playfield feedback success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not find playfield to feedback",
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

        [HttpPut(APIRoutes.PlayFieldFeedback.Update, Name ="Update PlayField Feedback")]
        public async Task<IActionResult> UpdatePlayFieldFeedback([FromBody] UpdatePlayFieldFeedback updatePlayFieldFeedback)
        {
            try
            {
                var result = await _playFieldFeedbackService.UpdatePlayFieldFeedback(updatePlayFieldFeedback);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Update PlayField Feedback Success",
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not find play field feedback for update",
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
             
        [HttpPut(APIRoutes.PlayFieldFeedback.UploadImage, Name = "Upload Image For PlayField Feedback")]
        public async Task<IActionResult> UploadImageForPlayFieldFeedback(IFormFile imageFeedback)
        {
            try
            {
                var result = await _playFieldFeedbackService.UploadImageFeedback(imageFeedback);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Upload image for playfield feedback success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not upload image for playfield feedback",
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

        [HttpPut(APIRoutes.PlayFieldFeedback.UploadVideo, Name = "Upload Video For PlayField Feedback")]
        public async Task<IActionResult> UploadVideoForPlayFieldFeedback(IFormFile videoFeedback)
        {
            try
            {
                var result = await _playFieldFeedbackService.UploadVideoFeedback(videoFeedback);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Upload video for playfield feedback success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not upload video for playfield feedback",
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

        [HttpDelete(APIRoutes.PlayFieldFeedback.Delete, Name = "Delete PlayField Feedback by PlayFieldId")]
        public async Task<IActionResult> DeletePlayFieldFeedbackByPlayFieldId([FromRoute] int playfieldFeedbackId)
        {
            try
            {
                var result = await _playFieldFeedbackService.DeletePlayfieldFeedback(playfieldFeedbackId);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Delete PlayField Feedback success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not delete PlayField Feedback",
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
