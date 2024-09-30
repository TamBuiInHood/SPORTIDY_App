using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.ClubModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FSU.SPORTIDY.Service.Services
{
    public class ClubService : IClubService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public ClubService(IUnitOfWork unitOfWork, IMapper mapper) 
        {  
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClubModel> CreateClub(CreateClubModel clubModel, int UserId)
        {
            try
            {
                var checkUser = await _unitOfWork.UserRepository.GetUserByIdAsync(UserId);
                if (checkUser != null)
                {
                    var newClub = _mapper.Map<Club>(clubModel);
                    newClub.ClubCode = Guid.NewGuid().ToString();
                    newClub.CreateDate = DateOnly.FromDateTime(DateTime.Now);


                    await _unitOfWork.ClubRepository.Insert(newClub);
                    var result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        var getClub = await _unitOfWork.ClubRepository.GetAllClub();
                        var newewstClub = getClub.OrderByDescending(x => x.ClubId).FirstOrDefault();


                        var userClub = new UserClub()
                        {
                            ClubId = newewstClub.ClubId,
                            UserId = UserId,
                            IsLeader = true,
                        };
                        var insertToUserClub = await _unitOfWork.ClubRepository.InsertToUserClub(userClub);
                        if (insertToUserClub)
                        {
                            var entities = await _unitOfWork.ClubRepository.GetAllClub();
                            var responseNewewstClub = entities.OrderByDescending(x => x.ClubId).FirstOrDefault();
                            var responseClub = _mapper.Map<ClubModel>(responseNewewstClub);
                            return responseClub;
                        }
                        return null;
                    }
                    else
                    {
                        throw new Exception("Have an error when create a club. Please try again ");
                    }
                }
                throw new Exception("User does not exist, so can not create a club");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteClub(int clubId)
        {
            string includeProperties = "UserClubs";
            var EntityDelete = await _unitOfWork.ClubRepository.GetByCondition(x => x.ClubId == clubId, includeProperties: includeProperties);
            if (EntityDelete == null)
            {
                return false;
            }
            EntityDelete!.UserClubs.Clear();
            try
            {
                    // Create a Firebase Storage client
                    var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                    // Parse the image URL to get the file name
                    var fileNameAvatarClub = EntityDelete.AvartarClub.Substring(EntityDelete.AvartarClub.LastIndexOf('/') + 1);
                    fileNameAvatarClub = fileNameAvatarClub.Split('?')[0]; // Remove the query parameters
                    var encodedFileAvatarClub = Path.GetFileName(fileNameAvatarClub);
                    var fileNameAvatarClubOfficial = Uri.UnescapeDataString(encodedFileAvatarClub);


                    // Delete the avaatar club from Firebase Storage
                    var fileRefAvatar = firebaseStorage.Child(fileNameAvatarClubOfficial);
                    await fileRefAvatar.DeleteAsync();

                    var fileNameCoverImageClub = EntityDelete.CoverImageClub.Substring(EntityDelete.CoverImageClub.LastIndexOf('/') + 1);
                    fileNameCoverImageClub = fileNameCoverImageClub.Split('?')[0]; // Remove the query parameters
                    var encodedFileCoverImageClub = Path.GetFileName(fileNameCoverImageClub);
                    var fileNameCoverImageClubOfficial = Uri.UnescapeDataString(encodedFileCoverImageClub);


                    // Delete the cover image club from Firebase Storage
                    var fileRefCoverImage = firebaseStorage.Child(fileNameCoverImageClubOfficial);
                    await fileRefCoverImage.DeleteAsync();
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the deletion process
                throw new Exception($"Error deleting image: {ex.Message}");
            }
            _unitOfWork.ClubRepository.Delete(EntityDelete);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            
            return result;
        }

        public async Task<PageEntity<ClubModel>> GetAllClub(PaginationParameter paginationParameter)
        {
            Expression<Func<Club, bool>> filter = null!;
            Func<IQueryable<Club>, IOrderedQueryable<Club>> orderBy = null!;
            if (!paginationParameter.Search.IsNullOrEmpty())
            {
                int validInt = 0;
                var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                if (checkInt)
                {
                    filter = x => x.ClubId == validInt || x.TotalMember == validInt;
                }
                else
                {
                    filter = x => x.ClubCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.ClubName.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Regulation.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Infomation.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Slogan.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.MainSport.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Location.ToLower().Contains(paginationParameter.Search.ToLower())
                    ;
                }
            }
            switch (paginationParameter.SortBy)
            {
                case "clubid":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.ClubId)
                               : x => x.OrderBy(x => x.ClubId)) : x => x.OrderBy(x => x.ClubId);
                    break;
                case "clubcode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.ClubCode)
                               : x => x.OrderBy(x => x.ClubCode)) : x => x.OrderBy(x => x.ClubCode);
                    break;
                case "clubname":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.ClubName)
                               : x => x.OrderBy(x => x.ClubName)) : x => x.OrderBy(x => x.ClubName);
                    break;
                case "regulation":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Regulation)
                               : x => x.OrderBy(x => x.Regulation)) : x => x.OrderBy(x => x.Regulation);
                    break;
                case "information":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Infomation)
                               : x => x.OrderBy(x => x.Infomation)) : x => x.OrderBy(x => x.Infomation);
                    break;
                case "slogan":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Slogan)
                               : x => x.OrderBy(x => x.Slogan)) : x => x.OrderBy(x => x.Slogan);
                    break;
                case "mainsport":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.MainSport)
                               : x => x.OrderBy(x => x.MainSport)) : x => x.OrderBy(x => x.MainSport);
                    break;
                case "location":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Location)
                               : x => x.OrderBy(x => x.Location)) : x => x.OrderBy(x => x.Location);
                    break;
                default:
                    orderBy = x => x.OrderByDescending(x => x.CreateDate);
                    break;
            }
            var entities = await _unitOfWork.ClubRepository.GetAllClub(filter, orderBy, paginationParameter.PageIndex, paginationParameter.PageSize);
            var pagin = new PageEntity<ClubModel>();
            pagin.List = _mapper.Map<IEnumerable<ClubModel>>(entities).ToList();
            pagin.TotalRecord = await _unitOfWork.ClubRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
            return pagin;
        }

        public async Task<List<ClubModel>> GetClubJoinedByUserId(int userId)
        {
            try
            {
                var result = await _unitOfWork.ClubRepository.GetClubJoinedByUserId(userId);
                var response = _mapper.Map<List<ClubModel>>(result);
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MeetingModel>> GetMeetingsByClubId(int clubId)
        {
            try
            {
                var result = await _unitOfWork.ClubRepository.GetMeetingsByClubId(clubId);
                var response = _mapper.Map<List<MeetingModel>>(result);
                return response;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> JoinedClub(int userId, int clubId)
        {
            try
            {
                var result = await _unitOfWork.ClubRepository.JoinedClub(userId, clubId);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateClub(UpdateClubModel updateClub)
        {
            var oldClub = await _unitOfWork.ClubRepository.GetByID(updateClub.clubId);
            if(oldClub != null)
            {
                oldClub.ClubName = updateClub.clubName;
                oldClub.AvartarClub = updateClub.avartarClub;
                oldClub.CoverImageClub = updateClub.coverImageClub;
                oldClub.Infomation = updateClub.infomation;
                oldClub.Slogan = updateClub.slogan;
                oldClub.Regulation = updateClub.regulation;
                oldClub.MainSport = updateClub.mainSport;
                oldClub.TotalMember = updateClub.totalMember;
                oldClub.Location = updateClub.location;
                 _unitOfWork.ClubRepository.Update(oldClub);
                var result = await _unitOfWork.SaveAsync();
                return result > 0;
            }
            return false;

        }
        public async Task<ClubModel> GetClubById(int clubId)
        {
            var entities = await _unitOfWork.ClubRepository.GetAllClub();
            var entity = entities.FirstOrDefault(x => x.ClubId == clubId);
            return _mapper?.Map<ClubModel?>(entity)!;
        }

        public async Task<string> UpdateAvatarClub(IFormFile avartarClub)
        {
            try
            {
                    string fileName = Path.GetFileName(avartarClub.FileName);
                    var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                    await firebaseStorage.Child("club/avatar").Child(fileName).PutAsync(avartarClub.OpenReadStream());
                    var downloadUrl = await firebaseStorage.Child("club/avatar").Child(fileName).GetDownloadUrlAsync();
                    return downloadUrl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateCoverImageClub(IFormFile coverImageClub)
        {
            try
            {
                    string fileName = Path.GetFileName(coverImageClub.FileName);
                    var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                    await firebaseStorage.Child("club/cover_image").Child(fileName).PutAsync(coverImageClub.OpenReadStream());
                    var downloadUrl = await firebaseStorage.Child("club/cover_image").Child(fileName).GetDownloadUrlAsync();
                    return downloadUrl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
