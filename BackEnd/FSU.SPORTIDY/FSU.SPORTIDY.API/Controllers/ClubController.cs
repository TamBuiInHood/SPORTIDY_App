using FSU.SmartMenuWithAI.API.Payloads.Responses;
using FSU.SPORTIDY.API.Payloads;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.ClubModels;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSU.SPORTIDY.API.Controllers
{
    [Route("api/club")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        public IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet(APIRoutes.Club.GetAll, Name ="Get All Club")]
        public async Task<IActionResult> GetAllClub(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _clubService.GetAllClub(paginationParameter);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get All Club success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Not found any club",
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

        [HttpGet(APIRoutes.Club.GetClubJoinedByUserId, Name ="Get Club Joined By UserId")]
        public async Task<IActionResult> GetClubJoinedByUserId([FromRoute] int userId)
        {
            try
            {
                var result = await _clubService.GetClubJoinedByUserId(userId);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get clubs of user success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "User does not have any club",
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

        [HttpGet(APIRoutes.Club.GetAllMeetingsOfClub, Name ="Get All Meetings Of Club")]
        public async Task<IActionResult> GetAllMettingOfClub([FromRoute] int clubId)
        {
            try
            {
                var result = await _clubService.GetMeetingsByClubId(clubId);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get all meeting of club success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "This club does not have any meeting",
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

        [HttpPost(APIRoutes.Club.Add, Name ="Create A Club")]
        public async Task<IActionResult> CreateClub([FromBody] CreateClubModel createClubModel, int currentUserId)
        {
            try
            {
                var result = await _clubService.CreateClub(createClubModel, currentUserId);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Create a club success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return BadRequest(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Create a club failed",
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

        [HttpPost(APIRoutes.Club.JoinedClub, Name ="Joinned Club")]
        public async Task<IActionResult> JoinnedClub(int userId, int clubId)
        {
            try
            {
                var result = await _clubService.JoinedClub(userId, clubId);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Joinned club success",
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Joinned club failed",
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

        [HttpPut(APIRoutes.Club.Update, Name ="Update Club")]
        public async Task<IActionResult> UpdateClub([FromBody] UpdateClubModel updateClubModel)
        {
            try
            {
                var result = await _clubService.UpdateClub(updateClubModel);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Update club success",
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Update club failed",
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

        [HttpDelete(APIRoutes.Club.Delete, Name = "Delete Club")]
        public async Task<IActionResult> DeleteClub([FromRoute] int id)
        {
            try
            {
                var result = await _clubService.DeleteClub(id);
                if(result)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Delete club success",
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Delete club failed",
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

        [HttpGet(APIRoutes.Club.GetByID, Name ="Get Club By Id")]
        public async Task<IActionResult> GetClubById([FromRoute] int clubId)
        {
            try
            {
                var result = await _clubService.GetClubById(clubId);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get Club By Id success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not get club with that id",
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


        [HttpPatch(APIRoutes.Club.UploadAvatarClub, Name ="Update Avatar Club")]
        public async Task<IActionResult> UpdateAvatarClub(IFormFile avartarClub)
        {
            if (avartarClub == null || avartarClub.Length == 0)
                return BadRequest("No file uploaded.");
            try
            {
                var result = await _clubService.UpdateAvatarClub(avartarClub);  
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Update avatar club success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not find club to update club",
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

        [HttpPatch(APIRoutes.Club.UploadCoverImageClub, Name ="Update Cover Image Club")]
        public async Task<IActionResult> UpdateCoverImageClub(IFormFile coverImageClub)
        {
            if (coverImageClub == null || coverImageClub.Length == 0)
                return BadRequest("No file uploaded.");
            try
            {
                var result = await _clubService.UpdateCoverImageClub(coverImageClub);   
                if (result != null) 
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Update Cover Image Club",
                        Data = result,
                        IsSuccess = true
                    });
                }
                else
                {
                    return NotFound(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Can not find club to update cover image",
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
