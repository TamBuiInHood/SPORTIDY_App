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
                playfieldModel.PlayFieldName = reqObj.PlayFieldName;
                playfieldModel.Address = reqObj.Address;
                playfieldModel.CloseTime = TimeOnly.FromDateTime(reqObj.CloseTime!.Value);
                playfieldModel.OpenTime = TimeOnly.FromDateTime(reqObj.OpenTime!.Value);
                playfieldModel.SportId = reqObj.sportId;
                playfieldModel.Price = reqObj.Price;

                var listImage = new List<IFormFile>();
                reqObj.AddImageField.OrderBy(x => x.ImageIndex);
                listImage.AddRange(reqObj.AddImageField.Select(x => x.ImageUrl)!);

                var playfieldInsert = await _playFieldService.CreatePlayField(playfieldModel, listImage, reqObj.AvatarImage!, reqObj.subPlayfieds);
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
        public async Task<IActionResult> DeleteAsynce([FromQuery(Name = "playfied-id")] int playfieldId)
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
        public async Task<IActionResult> UpdatePlayfieldAsync([FromQuery(Name = "playfield-id")] int playfieldId, [FromBody] UpdatePlayfield reqObj)
        {
            try
            {
                var dto = new PlayFieldModel();
                dto.PlayFieldId = playfieldId;
                dto.PlayFieldName = reqObj.PlayFieldName;
                dto.CloseTime = reqObj.CloseTime;
                dto.OpenTime = reqObj.OpenTime;
                dto.Price = reqObj.Price;

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
        public async Task<IActionResult> UpdateAvatarPlayfieldAsync([FromQuery(Name = "playfield-id")] int playfieldId, [FromForm] IFormFile avatarImage)
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
        public async Task<IActionResult> UpdatestatusPlayfieldAsync([FromQuery(Name = "playfield-id")] int playfieldId, [FromBody] int Status)
        {
            try
            {

                var result = await _playFieldService.UpdateStatusPlayfield(playfieldId, Status);
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
        public async Task<IActionResult> GetPlayfieldAsync([FromQuery(Name = "search-key")] string? searchKey
           , [FromQuery(Name = "page-number")] int pageNumber = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "page-size")] int PageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var getPlayfield = await _playFieldService.GetAllPlayField(searchKey!, pageIndex: pageNumber, pageSize: PageSize);

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
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "playfield-id")] int playfieldId)
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
        public async Task<IActionResult> GetByUserIdAsync([FromRoute(Name = "user-id")] int userId
           , [FromQuery(Name = "page-index")] int pageIndex = Page.DEFAULT_PAGE_INDEX
           , [FromQuery(Name = "page-size")] int PageSize = Page.DEFAULT_PAGE_SIZE)
        {
            try
            {
                var getPlayfield = await _playFieldService.GetPlayFieldsByUserId(userId, pageIndex: pageIndex, pageSize: PageSize);

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
    }
}
