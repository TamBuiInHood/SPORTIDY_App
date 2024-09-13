using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.SystemFeedbackModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FSU.SPORTIDY.Service.Services
{
    public class SystemFeedbackService : ISystemFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SystemFeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SystemFeedbackModel> CreateFeedback(CreateSystemFeedbackModel model)
        {
            try
            {
                var checkExistUser = await _unitOfWork.UserRepository.GetUserByIdAsync(model.UserId);
                if (checkExistUser != null)
                {
                    var newSystemFeedback = _mapper.Map<SystemFeedback>(model);
                    newSystemFeedback.FeedbackDate = DateTime.Now;
                    newSystemFeedback.FeedbackCode = Guid.NewGuid().ToString();
                    await _unitOfWork.SystemFeedbackRepository.Insert(newSystemFeedback);
                    var result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        var resultModel = _mapper.Map<SystemFeedbackModel>(newSystemFeedback);
                        if (model.IsAnonymous == true)
                        {
                            resultModel.UserFullName = null;
                        }
                        return resultModel;
                    }
                    else
                    {
                        throw new Exception($"Can not create system feedback");
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteSystemFeedback(int systemFeedbackId)
        {
            var EntityDelete = await _unitOfWork.SystemFeedbackRepository.GetByCondition(x => x.FeedbackId == systemFeedbackId);
            if (EntityDelete == null)
            {
                return false;
            }
            try
            {
                // Create a Firebase Storage client
                var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                if (EntityDelete.ImageUrl != null)
                {
                    // Parse the image URL to get the file name
                    var fileNameImage = EntityDelete.ImageUrl.Substring(EntityDelete.ImageUrl.LastIndexOf('/') + 1);
                    fileNameImage = fileNameImage.Split('?')[0]; // Remove the query parameters
                    var encodedFileImage = Path.GetFileName(fileNameImage);
                    var fileNameImageOfficial = Uri.UnescapeDataString(encodedFileImage);


                    // Delete the avatar club from Firebase Storage
                    var fileRefImage = firebaseStorage.Child(fileNameImageOfficial);
                    await fileRefImage.DeleteAsync();
                }
                if (EntityDelete.VideoUrl != null)
                {
                    var fileNameVideo = EntityDelete.VideoUrl.Substring(EntityDelete.VideoUrl.LastIndexOf('/') + 1);
                    fileNameVideo = fileNameVideo.Split('?')[0]; // Remove the query parameters
                    var encodedFileVideo = Path.GetFileName(fileNameVideo);
                    var fileNameVideoOfficial = Uri.UnescapeDataString(encodedFileVideo);


                    // Delete the cover image club from Firebase Storage
                    var fileRefVideo = firebaseStorage.Child(fileNameVideoOfficial);
                    await fileRefVideo.DeleteAsync();
                }

            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the deletion process
                throw new Exception($"Error deleting image: {ex.Message}");
            }
            _unitOfWork.SystemFeedbackRepository.Delete(systemFeedbackId);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<PageEntity<SystemFeedbackModel>> GetAllSystemFeedback(PaginationParameter paginationParameter)
        {
            Expression<Func<SystemFeedback, bool>> filter = null!;
            Func<IQueryable<SystemFeedback>, IOrderedQueryable<SystemFeedback>> orderBy = null!;
            if (!paginationParameter.Search.IsNullOrEmpty())
            {
                int validInt = 0;
                var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                if (checkInt)
                {
                    filter = x => x.FeedbackId == validInt;
                }
                else
                {
                    filter = x => x.FeedbackCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Content.ToLower().Contains(paginationParameter.Search.ToLower());
                }
            }
            switch (paginationParameter.SortBy)
            {
                case "feedbackid":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.FeedbackId)
                               : x => x.OrderBy(x => x.FeedbackId)) : x => x.OrderBy(x => x.FeedbackId);
                    break;
                case "feedbackcode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.FeedbackCode)
                               : x => x.OrderBy(x => x.FeedbackCode)) : x => x.OrderBy(x => x.FeedbackCode);
                    break;
                case "content":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Content)
                               : x => x.OrderBy(x => x.Content)) : x => x.OrderBy(x => x.Content);
                    break;
                case "rating":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Rating)
                               : x => x.OrderBy(x => x.Rating)) : x => x.OrderBy(x => x.Rating);
                    break;
                case "feebackdate":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.FeedbackDate)
                               : x => x.OrderBy(x => x.FeedbackDate)) : x => x.OrderBy(x => x.FeedbackDate);
                    break;
                case "isanonymous":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.IsAnonymous)
                               : x => x.OrderBy(x => x.IsAnonymous)) : x => x.OrderBy(x => x.IsAnonymous);
                    break;
                default:
                    orderBy = x => x.OrderByDescending(x => x.FeedbackDate);
                    break;
            }
            string includeProperties = "User";
            var entities = await _unitOfWork.SystemFeedbackRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
            var pagin = new PageEntity<SystemFeedbackModel>();
            pagin.List = _mapper.Map<IEnumerable<SystemFeedbackModel>>(entities).ToList();
            pagin.TotalRecord = await _unitOfWork.SystemFeedbackRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);

            foreach(var item in pagin.List)
            {
                if(item.IsAnonymous == true)
                {
                    item.UserFullName = null;
                    item.Avatar = null;
                }
            }
            return pagin;
        }
        public async Task<PageEntity<SystemFeedbackModel>> GetAllSystemFeedbackWithNoPaging()
        {
            string includeProperties = "User";
            var entities = await _unitOfWork.SystemFeedbackRepository.GetAllNoPaging(includeProperties: includeProperties);
            var pagin = new PageEntity<SystemFeedbackModel>();
            pagin.List = _mapper.Map<IEnumerable<SystemFeedbackModel>>(entities).ToList();
            pagin.TotalRecord = await _unitOfWork.SystemFeedbackRepository.Count();
            pagin.TotalPage = 1;
            foreach (var item in pagin.List)
            {
                if (item.IsAnonymous == true)
                {
                    item.UserFullName = null;
                    item.Avatar = null;
                }
            }
            return pagin;
        }


        public async Task<SystemFeedbackModel> GetSystemFeedbackById(int feedbackId)
        {
            string includeProperties = "User";
            var systemFeedback = await _unitOfWork.SystemFeedbackRepository.GetByCondition(x => x.FeedbackId == feedbackId, includeProperties);
            var result = _mapper.Map<SystemFeedbackModel>(systemFeedback);

            try
            {
                var customerFeedback = await _unitOfWork.UserRepository.GetUserByIdAsync(systemFeedback.UserId);

                result.UserFullName = customerFeedback.FullName;
                if (result.IsAnonymous == true)
                {
                    result.UserFullName = null;
                    result.Avatar = null;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<List<SystemFeedbackModel>> GetSystemFeedbackByUserId(int userId)
        {
            var listFeedback = await _unitOfWork.SystemFeedbackRepository.GetSystemFeedbackByUserId(userId);
            var result = _mapper.Map<List<SystemFeedbackModel>>(listFeedback);
            foreach ( var item in result)
            {
                if(item.IsAnonymous == true)
                {
                    item.UserFullName = null;
                    item.Avatar = null;
                }
            }
            return result;
        }

        public async Task<SystemFeedbackDashBoard> GetSystemFeedbackDashBoard()
        {
            var systemFeedbackDashboard = new SystemFeedbackDashBoard();
            (int TotalFeedbacks, int TotalImages, int TotalVideos, int TotalRatings) getAllSystemFeedback = await  _unitOfWork.SystemFeedbackRepository.GetFeedbackDashboard(); 
            systemFeedbackDashboard.TotalFeedback = getAllSystemFeedback.TotalFeedbacks;
            systemFeedbackDashboard.TotalImage = getAllSystemFeedback.TotalImages;
            systemFeedbackDashboard.TotalVideo = getAllSystemFeedback.TotalVideos;
            systemFeedbackDashboard.TotalRating = getAllSystemFeedback.TotalRatings;
            return systemFeedbackDashboard;
        }

        public async Task<bool> UpdateSystemFeedback(UpdateSystemFeedbackModel updateSystemFeedback)
        {
            var oldFeedback = await _unitOfWork.SystemFeedbackRepository.GetByID(updateSystemFeedback.FeedbackId);
            if (oldFeedback != null)
            {
                oldFeedback.FeedbackCode = updateSystemFeedback.FeedbackCode;
                oldFeedback.Content = updateSystemFeedback.Content;
                oldFeedback.Rating = updateSystemFeedback.Rating;
                oldFeedback.FeedbackDate = updateSystemFeedback.FeedbackDate;
                oldFeedback.ImageUrl = updateSystemFeedback.ImageUrl;
                oldFeedback.VideoUrl = updateSystemFeedback.VideoUrl;
                oldFeedback.IsAnonymous = updateSystemFeedback.IsAnonymous;
                var result = await _unitOfWork.SaveAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<string> UploadImageSystemFeedback(IFormFile imageSystemFeedback)
        {
            string fileName = Path.GetFileName(imageSystemFeedback.FileName);
            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
            await firebaseStorage.Child("system_feedback/image").Child(fileName).PutAsync(imageSystemFeedback.OpenReadStream());
            var downloadUrl = await firebaseStorage.Child("system_feedback/image").Child(fileName).GetDownloadUrlAsync();
            return downloadUrl;
        }

        public async Task<string> UploadVideoSystenFeedback(IFormFile videoSystemFeedback)
        {
            string videoFileName = Path.GetFileName(videoSystemFeedback.FileName);
            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
            await firebaseStorage.Child("system_feedback/video").Child(videoFileName).PutAsync(videoSystemFeedback.OpenReadStream());
            var downloadVideoUrl = await firebaseStorage.Child("system_feedback/video").Child(videoFileName).GetDownloadUrlAsync();
            return downloadVideoUrl; ;
        }
    }
}
