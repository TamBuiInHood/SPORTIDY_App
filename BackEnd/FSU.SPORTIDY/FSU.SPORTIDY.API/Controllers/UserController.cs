﻿using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.API.Payloads.Request;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSU.SPORTIDY.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(APIRoutes.User.GetAll, Name ="GetAllUser")]
       public async Task<IActionResult> GetAllUser(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _userService.GetAllUser(paginationParameter);
                if(result.List.Count() > 0)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get all user success",
                        Data = result.List,
                        IsSuccess = true
                    });
                } 
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No user found",
                        Data = result.List,
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

        [HttpGet(APIRoutes.User.GetByID, Name ="GetUserByID")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get user by user id successfully",
                        Data = result,
                        IsSuccess = true
                    });
                } 
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not found user with that id",
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

        [HttpGet(APIRoutes.User.GetByEmail, Name ="GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            try
            {
                var result = await _userService.GetUserByEmail(email);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get user by email successfully",
                        Data = result,
                        IsSuccess = true
                    });
                } else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not found user with that email",
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
        [HttpPost(APIRoutes.User.Add, Name ="CreateUser")]
        public async Task<IActionResult> CreateUserInternal([FromBody] CreateAccountModel createAccountRequestModel)
        {
            try
            {
                var result = await _userService.CreateUser(createAccountRequestModel);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Create new user success",
                        Data = result,
                        IsSuccess=true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not create new user",
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
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UpdateUserModel updateUserRequestModel)
        {
            try
            {
                var result = await _userService.UpdateUser(updateUserRequestModel);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Data = result,
                        Message = "Update account success",
                        IsSuccess = true
                    });
                } 
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Account does not exist. Can not update",
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
        [HttpPost("banned-user/{userId}")]
        public async Task<IActionResult> BannedUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.BannedUser(userId);
                if (result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Banned user success",
                        IsSuccess = true,
                        Data = result,
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not found user for banned",
                        Data = result,
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

        [HttpDelete("soft-delete-user/{userId}")]
        public async Task<IActionResult> SoftDeleteUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.SoftDeleteUser(userId);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Soft Delete user success",
                        IsSuccess = true,
                        Data = result,
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not found user for delete",
                        Data = result,
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

        [HttpDelete(APIRoutes.User.Delete, Name ="DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Delete User success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not found user to delete",
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
