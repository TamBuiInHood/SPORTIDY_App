using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using Microsoft.AspNetCore.Mvc;
using FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels;
using FSU.SPORTIDY.API.Validations;
using FSU.SPORTIDY.Common.Status;

namespace FSU.SPORTIDY.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PlayfieldController : ControllerBase
    {
        private readonly IPlayFieldService _playFieldService;
        private readonly ImageFileValidator _imageFileValidator;

        public PlayfieldController(IPlayFieldService playFieldService)
        {
            _playFieldService = playFieldService;
            _imageFileValidator = new ImageFileValidator();
        }


        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost(APIRoutes.Playfields.Add, Name = "AddPlayfieldAsync")]
        public async Task<IActionResult> AddAsync([FromForm] AddPlayfiedRequest reqObj)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(new BaseResponse
                //    {
                //        StatusCode = StatusCodes.Status400BadRequest,
                //        Message = "your information are not correct",
                //        Data = ModelState.Values.SelectMany(v => v.Errors),
                //        IsSuccess = false
                //    });
                //}

                var playfieldModel = new PlayFieldModel();
                playfieldModel.PlayFieldName = reqObj.playfieldName;
                playfieldModel.Address = reqObj.address;
                playfieldModel.CloseTime = TimeOnly.FromDateTime(reqObj.closeTime!.Value);
                playfieldModel.OpenTime = TimeOnly.FromDateTime(reqObj.openTime!.Value);
                playfieldModel.SportId = reqObj.sportId;
                playfieldModel.Price = reqObj.price;
                playfieldModel.UserId = reqObj.currentIdLogin;

                var listImage = new List<IFormFile>();
                reqObj.addImageField.OrderBy(x => x.ImageIndex);
                listImage.AddRange(reqObj.addImageField.Select(x => x.ImageUrl)!);

                var playfieldInsert = await _playFieldService.CreatePlayField(playfieldModel, listImage, reqObj.avatarImage!, reqObj.subPlayfieds);
                if (playfieldInsert == null)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Create PlayField fail.",
                        Data = null,
                        IsSuccess = false
                    });
                }
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Creat Playfield Successfully",
                    Data = playfieldInsert,
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
        [HttpDelete(APIRoutes.Playfields.Delete, Name = "DeletePlatfiedAsync")]
        public async Task<IActionResult> DeleteAsynce([FromQuery(Name = "playfiedId")] int playfieldId)
        {
            try
            {
                var result = await _playFieldService.DeletePlayField(playfieldId);
                if (!result)
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Playfield not found",
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

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut(APIRoutes.Playfields.Update, Name = "UpdatePlayfieldAsync")]
        public async Task<IActionResult> UpdatePlayfieldAsync([FromQuery(Name = "playfieldId")] int playfieldId, [FromBody] UpdatePlayfield reqObj)
        {
            try
            {
                var dto = new PlayFieldModel();
                dto.PlayFieldId = playfieldId;
                dto.PlayFieldName = reqObj.playfieldName;
                dto.CloseTime = reqObj.closeTime;
                dto.OpenTime = reqObj.openTime;
                dto.Price = reqObj.price;

                var result = await _playFieldService.UpdatePlayField(dto);
                if (result == false)
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

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut(APIRoutes.Playfields.UpdateAvatar, Name = "UpdateAvatarPlayfieldAsync")]
        public async Task<IActionResult> UpdateAvatarPlayfieldAsync([FromQuery(Name = "playfieldId")] int playfieldId, [FromForm] IFormFile avatarImage)
        {
            try
            {
                var playfieldExist = _playFieldService.UpdateAvatarImage(avatarImage,playfieldId);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Update successfully",
                    Data = true,
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
        [HttpPut(APIRoutes.Playfields.UpdateStatus, Name = "UpdateStatusPlayfieldAsync")]
        public async Task<IActionResult> UpdatestatusPlayfieldAsync([FromQuery(Name = "playfieldId")] int playfieldId, [FromBody] int status)
        {
            try
            {

                var result = await _playFieldService.UpdateStatusPlayfield(playfieldId, status);
                if (result == false)
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

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet(APIRoutes.Playfields.GetAll, Name = "GetPlayfieldAsync")]
        public async Task<IActionResult> GetPlayfieldAsync([FromQuery(Name = "searchKey")] string? searchKey
           , [FromQuery(Name = "pageIndex")] int pageIndex = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "pageSize")] int pageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var getPlayfield = await _playFieldService.GetAllPlayField(searchKey!, pageIndex: pageIndex, pageSize: pageSize);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Load successfully",
                    Data = getPlayfield,
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
        [HttpGet(APIRoutes.Playfields.GetByID, Name = "GetPlayfieldByIdAsync")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "playfieldId")] int playfieldId)
        {
            try
            {
                var getPlayfield = await _playFieldService.GetPlayFieldById(playfieldId);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Load successfully",
                    Data = getPlayfield,
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
        [HttpGet(APIRoutes.Playfields.GetByUserID, Name = "GetPlayfieldByUserIdAsync")]
        public async Task<IActionResult> GetByUserIdAsync([FromRoute(Name = "userId")] int userId
           , [FromQuery(Name = "pageIndex")] int pageIndex = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "pageSize")] int pageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var getPlayfield = await _playFieldService.GetPlayFieldsByUserId(userId, pageIndex: pageIndex, pageSize: pageSize);

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Load successfully",
                    Data = getPlayfield,
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
        [HttpPut(APIRoutes.Playfields.UpdateForAdmin, Name = "UpdatePlayfieldForAdminAsync")]
        public async Task<IActionResult> UpdatePlayfieldForAdminAsync([FromRoute(Name = "playfieldId")] int playfieldId, [FromBody] UpdatePlayfield reqObj)
        {
            try
            {
                var dto = new PlayFieldModel();
                dto.PlayFieldId = playfieldId;
                dto.PlayFieldName = reqObj.playfieldName;
                dto.Status = reqObj.status;
                dto.Price = reqObj.price;
                dto.Address = reqObj.address;

                var result = await _playFieldService.UpdatePlayFieldForAdmin(dto);
                if (result == false)
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
    }
}
