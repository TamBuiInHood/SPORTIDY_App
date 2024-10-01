using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ImageFieldController : ControllerBase
    {
        private readonly IImageFieldService _imageFieldService;

        public ImageFieldController(IImageFieldService imageFieldService)
        {
            _imageFieldService = imageFieldService;
        }

        //[Authorize(Roles = $"{UserRoleConst.ADMIN},{UserRoleConst.SPORTOWNER}")]
        [HttpDelete(APIRoutes.ImageField.Delete)]
        public async Task<IActionResult> DeleteImage([FromRoute(Name = "imageId")] int imageId)
        {
            try
            {
                var result = await _imageFieldService.Delete(imageId);
                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Image not found or could not be deleted",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Image deleted successfully",
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

        //[Authorize(Roles = $"{UserRoleConst.ADMIN},{UserRoleConst.SPORTOWNER}")]
        [HttpGet(APIRoutes.ImageField.GetAll)]
        public async Task<IActionResult> GetImages([FromRoute(Name = "playfieldId")] int playfieldId)
        {
            try
            {
                var images = await _imageFieldService.Get(playfieldId);
                if (images == null || !images.Any())
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No images found for this playfield",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Images retrieved successfully",
                    Data = images,
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

        //[Authorize(Roles = $"{UserRoleConst.ADMIN},{UserRoleConst.SPORTOWNER}")]
        [HttpGet(APIRoutes.ImageField.GetByID)]
        public async Task<IActionResult> GetImageById([FromRoute(Name = "imageId")] int imageId)
        {
            try
            {
                var image = await _imageFieldService.GetById(imageId);
                if (image == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Image not found",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Image retrieved successfully",
                    Data = image,
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

        //[Authorize(Roles = $"{UserRoleConst.ADMIN},{UserRoleConst.SPORTOWNER}")]
        [HttpPost(APIRoutes.ImageField.Add)]
        public async Task<IActionResult> InsertImage([FromForm] IFormFile imageUrl, int playfieldId)
        {
            try
            {
                var image = await _imageFieldService.Insert(imageUrl, playfieldId);
                if (image == null)
                {
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Could not insert image",
                        Data = null,
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Image inserted successfully",
                    Data = image,
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
