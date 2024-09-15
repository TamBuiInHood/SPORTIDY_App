using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.SystemFeedbackModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FSU.SPORTIDY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemFeedbackController : ControllerBase
    {
        public ISystemFeedbackService _systemFeedbackService;

        public SystemFeedbackController(ISystemFeedbackService systemFeedbackService)
        {
            _systemFeedbackService = systemFeedbackService;
        }

        [HttpGet(APIRoutes.SystemFeedback.GetAll, Name ="Get All System Feedback")]
        public async Task<IActionResult> GetAllSystemFeedback(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _systemFeedbackService.GetAllSystemFeedback(paginationParameter);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get All System Feedback Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Can Not Get All System Feedback",
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

        [HttpGet(APIRoutes.SystemFeedback.GetAllNoPaging, Name = "Get All System Feedback With No Paging")]
        public async Task<IActionResult> GetAllSystemFeedbackWithNoPaging()
        {
            try
            {
                var result = await _systemFeedbackService.GetAllSystemFeedbackWithNoPaging();
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get All System Feedback Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Can Not Get All System Feedback With No Paging",
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

        [HttpGet(APIRoutes.SystemFeedback.GetByID, Name = "Get System Feedback By Id")]
        public async Task<IActionResult> GetSystemFeedbackById([FromRoute] int systemFeedbackId)
        {
            try
            {
                var result = await _systemFeedbackService.GetSystemFeedbackById(systemFeedbackId);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get System Feedback By Id Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Can Not Find Any System Feedback With That Id",
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

        [HttpGet(APIRoutes.SystemFeedback.GetByUserId, Name = "Get System Feedback By User Id")]
        public async Task<IActionResult> GetSystemFeedbackByUserId([FromRoute] int userId)
        {
            try
            {
                var result = await _systemFeedbackService.GetSystemFeedbackByUserId(userId);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get System Feedback By User Id Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Can Not Find Any System Feedback With That User Id",
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

        [HttpGet(APIRoutes.SystemFeedback.Dashboard, Name = "Get System Feedback Dashboard")]
        public async Task<IActionResult> GetSystemFeedbackDashboard()
        {
            try
            {
                var result = await _systemFeedbackService.GetSystemFeedbackDashBoard();
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get System Feedback Dashboard Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Can Not Get System Feedback Dashboard",
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

        [HttpPost(APIRoutes.SystemFeedback.Create, Name = "Create System Feedback")]
        public async Task<IActionResult> CreateSystemFeedback([FromBody] CreateSystemFeedbackModel model)
        {
            try
            {
                var result = await _systemFeedbackService.CreateFeedback(model);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Create System Feedback Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Can Not Create System Feedback",
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

        [HttpPut(APIRoutes.SystemFeedback.UploadImage, Name = "Upload Image For System Feedback")]
        public async Task<IActionResult> UploadImageForPSystemFeedback(IFormFile imageFeedback)
        {
            try
            {
                var result = await _systemFeedbackService.UploadImageSystemFeedback(imageFeedback);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Upload Image For System Feedback Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can Not Upload Image For System Feedback",
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

        [HttpPut(APIRoutes.SystemFeedback.UploadVideo, Name = "Upload Video For System Feedback")]
        public async Task<IActionResult> UploadVideoForSystemFeedback(IFormFile videoFeedback)
        {
            try
            {
                var result = await _systemFeedbackService.UploadVideoSystenFeedback(videoFeedback);
                if (result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Upload Video For System Feedback Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can Not Upload Video For System Feedback",
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

        [HttpPut(APIRoutes.SystemFeedback.Update, Name = "Update System Feedback")]
        public async Task<IActionResult> UpdateSystemFeedback([FromBody] UpdateSystemFeedbackModel model)
        {
            try
            {
                var result = await _systemFeedbackService.UpdateSystemFeedback(model);
                if (result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Update System Feedback Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Can Not Update System Feedback",
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

        [HttpDelete(APIRoutes.SystemFeedback.Delete, Name = "Delete System Feedback By SystemFeedbackId")]
        public async Task<IActionResult> DeleteSystemFeedbackBySystemFeedbackId([FromRoute] int systemFeedbackId)
        {
            try
            {
                var result = await _systemFeedbackService.DeleteSystemFeedback(systemFeedbackId);
                if (result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Delete System Feedback Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can Not Delete System Feedback",
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
