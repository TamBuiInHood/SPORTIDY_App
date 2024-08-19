using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads.Request;
using FSU.SPORTIDY.Service.BusinessModel.AuthensModel;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FSU.SPORTIDY.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        public IUserService _userService { get; set; }

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(SignUpModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _userService.RegisterAsync(model);
                    return Ok(new BaseResponse
                    {
                        Message = "Register success. You can login now",
                        IsSuccess = result,
                        StatusCode = StatusCodes.Status200OK,
                    });
                }
                return ValidationProblem(ModelState);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithEmail([FromBody] AccountRequestModel accountModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _userService.LoginByEmailAndPassword(accountModel.Email, accountModel.Password);
                    if(result.HttpCode == StatusCodes.Status200OK)
                    {
                        return Ok(result);
                    }
                    return Unauthorized(result);
                } 
                else
                {
                    return ValidationProblem(ModelState);
                }
            }
            catch (Exception ex)
            {
                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }

        }
    }
}
