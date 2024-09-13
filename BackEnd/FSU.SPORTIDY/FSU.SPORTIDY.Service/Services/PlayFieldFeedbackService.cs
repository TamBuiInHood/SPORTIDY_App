using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldFeedbackModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FSU.SPORTIDY.Service.Services
{
    public class PlayFieldFeedbackService : IPlayFieldFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlayFieldFeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PlayFieldFeedbackModel> CreateFeedback(CreatePlayFieldFeedbackModel model)
        {
            try
            {
                var checkExistPlayField = await _unitOfWork.PlayFieldFeedbackRepository.GetPlayFieldByPlayFieldId(model.PlayFieldId);
                if (checkExistPlayField != null)
                {
                    var newPlayFieldFeedback = _mapper.Map<PlayFieldFeedback>(model);
                    newPlayFieldFeedback.FeedbackDate = DateTime.Now;
                    newPlayFieldFeedback.FeedbackCode = Guid.NewGuid().ToString();
                    await _unitOfWork.PlayFieldFeedbackRepository.Insert(newPlayFieldFeedback);
                    var result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        var getPlayFieldFeedback = await _unitOfWork.PlayFieldFeedbackRepository.GetNewestPlayFieldFeedback();
                        var customerFeedback = await _unitOfWork.UserRepository.GetUserByIdAsync((int)getPlayFieldFeedback.Booking.CustomerId);

                        var resultModel = _mapper.Map<PlayFieldFeedbackModel>(getPlayFieldFeedback);
                        resultModel.UserFullName = customerFeedback.FullName;
                        if (model.IsAnonymous == true)
                        {
                            resultModel.UserFullName = null;
                        }
                        return resultModel;
                    }
                    else
                    {
                        throw new Exception($"Can not create play field feedback");
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

        public async Task<bool> DeletePlayfieldFeedback(int playFieldFeedbackId)
        {
            var EntityDelete = await _unitOfWork.PlayFieldFeedbackRepository.GetByCondition(x => x.FeedbackId == playFieldFeedbackId);
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
                if(EntityDelete.VideoUrl != null)
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
            _unitOfWork.PlayFieldFeedbackRepository.Delete(playFieldFeedbackId);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<PageEntity<PlayFieldFeedbackModel>> GetAllPlayFieldFeedback(PaginationParameter paginationParameter)
        {
            Expression<Func<PlayFieldFeedback, bool>> filter = null!;
            Func<IQueryable<PlayFieldFeedback>, IOrderedQueryable<PlayFieldFeedback>> orderBy = null!;
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
            string includeProperties = "Booking";
            var entities = await _unitOfWork.PlayFieldFeedbackRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
            var pagin = new PageEntity<PlayFieldFeedbackModel>();
            pagin.List = _mapper.Map<IEnumerable<PlayFieldFeedbackModel>>(entities).ToList();
            pagin.TotalRecord = await _unitOfWork.PlayFieldFeedbackRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);

            foreach (var item in entities.ToList())
            {
                var user = await _unitOfWork.UserRepository.GetByID((int)item.Booking.CustomerId);
                // Tìm phần tử tương ứng trong pagin.List
                var paginItem = pagin.List.FirstOrDefault(p => p.FeedbackId == item.FeedbackId);

                // Gán giá trị FullName từ user vào paginItem
                if (paginItem != null && user != null)
                {
                    paginItem.UserFullName = user.FullName;
                }

            }

            return pagin;
        }

        public async Task<PlayFieldFeedbackModel> GetPlayFieldFeedbackById(int feedbackId)
        {
            var playFieldFeedback = await _unitOfWork.PlayFieldFeedbackRepository.GetPlayFieldFeedbackByIdAsync(feedbackId);
            var result = _mapper.Map<PlayFieldFeedbackModel>(playFieldFeedback);

            var customerFeedback = await _unitOfWork.UserRepository.GetUserByIdAsync((int)playFieldFeedback.Booking.CustomerId);

            result.UserFullName = customerFeedback.FullName;
            if (result.IsAnonymous == true)
            {
                result.UserFullName = null;
            }
            return result;
        }

        public async Task<List<PlayFieldFeedbackModel>> GetPlayFieldFeedbackByOwnerId(int ownerId)
        {
            var result = await _unitOfWork.PlayFieldFeedbackRepository.GetPlayFieldFeedbackByOwnerId(ownerId);
            var resultModel = new List<PlayFieldFeedbackModel>();
            foreach (var item in result)
            {
                var customerFeedback = await _unitOfWork.UserRepository.GetUserByIdAsync((int)item.Booking.CustomerId);

                var resultItem = _mapper.Map<PlayFieldFeedbackModel>(item);
                resultItem.UserFullName = customerFeedback.FullName;
                if (item.IsAnonymous == true)
                {
                    resultItem.UserFullName = null;
                }
                resultModel.Add(resultItem);

            }
            return resultModel;
        }

        public async Task<bool> UpdatePlayFieldFeedback(UpdatePlayFieldFeedback updatePlayFieldFeedback)
        {
            var oldFeedback = await _unitOfWork.PlayFieldFeedbackRepository.GetByID(updatePlayFieldFeedback.FeedbackId);
            if (oldFeedback != null)
            {
                oldFeedback.FeedbackCode = updatePlayFieldFeedback.FeedbackCode;
                oldFeedback.Content = updatePlayFieldFeedback.Content;
                oldFeedback.Rating = updatePlayFieldFeedback.Rating;
                oldFeedback.FeedbackDate = updatePlayFieldFeedback.FeedbackDate;
                oldFeedback.ImageUrl = updatePlayFieldFeedback.ImageUrl;
                oldFeedback.VideoUrl = updatePlayFieldFeedback.VideoUrl;
                oldFeedback.IsAnonymous = updatePlayFieldFeedback.IsAnonymous;
                var result = await _unitOfWork.SaveAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<string> UploadImageFeedback(IFormFile imageFeedback)
        {
            string fileName = Path.GetFileName(imageFeedback.FileName);
            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
            await firebaseStorage.Child("playField_feedback/image").Child(fileName).PutAsync(imageFeedback.OpenReadStream());
            var downloadUrl = await firebaseStorage.Child("playField_feedback/image").Child(fileName).GetDownloadUrlAsync();
            return downloadUrl;
        }

        public async Task<string> UploadVideoFeedback(IFormFile videoFeedback)
        {
            string videoFileName = Path.GetFileName(videoFeedback.FileName);
            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
            await firebaseStorage.Child("playField_feedback/video").Child(videoFileName).PutAsync(videoFeedback.OpenReadStream());
            var downloadVideoUrl = await firebaseStorage.Child("playField_feedback/video").Child(videoFileName).GetDownloadUrlAsync();
            return downloadVideoUrl;
        }
    }
}
