using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Service.BusinessModel.NotificationModels;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FSU.SPORTIDY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet(APIRoutes.Notifcation.GetByEmail, Name = "Get Notification By Email")]
        public async Task<IActionResult> GetNotificationsByEmail([FromRoute] string email)
        {
            try
            {
                var result = await _notificationService.GetNotificationsByEmail(email);
                if (result.Count > 0)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get Notification By Email Success",
                        Data = result,
                        IsSuccess = true
                    });
                }

                return NotFound(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Notification is empty",
                    IsSuccess = false
                });

            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString(),
                        IsSuccess = false
                    }
               );
            }
        }

        [HttpGet(APIRoutes.Notifcation.GetByCustomerID, Name = "Get Notification By Customer Id")]
        public async Task<IActionResult> GetNotificationsByCustomerId([FromRoute(Name = "customerId")] int customerId)
        {
            try
            {
                var result = await _notificationService.GetNotificationsByCustomerId(customerId);
                if (result.Count > 0)
                {
                   
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get Notification By CustomerId Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                return NotFound(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Notification is empty",
                    IsSuccess = false
                });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString(),
                        IsSuccess = false
                    }
               );
            }
        }


        [HttpGet(APIRoutes.Notifcation.GetByID, Name = "GetNotificationByIdAsync")]
        public async Task<IActionResult> GetNotificationById([FromRoute(Name = "notificationId")] int id)
        {
            try
            {
                var result = await _notificationService.GetNotificationById(id);
                if (result == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not found notification.",
                        IsSuccess=false
                    });
                }
                return Ok(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get Notification By Id Success",
                    Data= result,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString(),
                        IsSuccess = false
                    }
               );
            }
        }

        [HttpPost(APIRoutes.Notifcation.AddByCustomerId, Name = "Add Notification By Customerr Id")]
        public async Task<IActionResult> AddNotificationByCustomerId([FromRoute(Name = "customerId")] int id, [FromBody] NotificationModel notificationModel)
        {
            try
            {
                var result = await _notificationService.AddNotificationByCustomerId(id, notificationModel);
                if (result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Add Notification By Customer Id Success",
                        Data = result,
                        IsSuccess = true
                    });
                   
                }
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Can not add notification."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }


        [HttpPost(APIRoutes.Notifcation.MarkAllCustomerNotificationIsReadByCustomerId, 
                  Name = "Mark All Customer  Notification Is Read By Customer Id")]
        public async Task<IActionResult> MarkAllCustomerNotificationIsReadByCustomerId([FromRoute(Name = "customerId")] int customerId)
        {
            try
            {
                var result = await _notificationService.MarkAllUserNotificationIsReadByUserIdAsync(customerId);
                if (result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Mark read all notifications by user id successfully."
                    });
                }
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Can not mark read all notifications."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }

        [HttpPost(APIRoutes.Notifcation.MarkNotificationIsReadByNotificationId,
                  Name = "Mark Notification Is Read By Notification Id")]
        public async Task<IActionResult> MarkAllNotificationIsReadByNotificationId([FromRoute(Name = "notificationId")] int notificationId)
        {
            try
            {
                var result = await _notificationService.MarkNotificationIsReadById(notificationId);
                if (result)
                {
                    return Ok(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Mark read notification successfully."
                    });
                }
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Can not mark read notification."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }

        [HttpPost(APIRoutes.Notifcation.AddByRole, Name = "Add Notification By Role")]
        public async Task<IActionResult> AddNotificationForRole([FromRoute(Name = "roleName")] string roleName, NotificationListModel notificationListModel)
        {
            try
            {
                var result = await _notificationService.AddNotificationByRoleAsync(roleName, notificationListModel);
                if (result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Add Notification By Role Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Can not add notification."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }

        [HttpPost(APIRoutes.Notifcation.AddByListUserId, Name = "Add Notification By List User Id")]
        public async Task<IActionResult> AddNotificationForListUserId(NotificationListModel notificationModel)
        {
            try
            {
                var result = await _notificationService.AddNotificationByListCustomerId(notificationModel.listUserId, notificationModel);
                if (result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Add Notification For List User Id Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Can not add notification."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }
    }
}
