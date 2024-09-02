using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.ClubModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldFeedbackModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IPlayFieldFeedbackService
    {
        public Task<PlayFieldFeedbackModel> CreateFeedback(CreatePlayFieldFeedbackModel model);
        public Task<string> UploadImageFeedback(IFormFile imageFeedback);
        public Task<string> UploadVideoFeedback(IFormFile videoFeedback);

        public Task<PageEntity<PlayFieldFeedbackModel>> GetAllPlayFieldFeedback(PaginationParameter paginationParameter);
        public Task<PlayFieldFeedbackModel> GetPlayFieldFeedbackById(int feedbackId);
        public Task<List<PlayFieldFeedbackModel>> GetPlayFieldFeedbackByOwnerId(int ownerId);
        public Task<bool> UpdatePlayFieldFeedback(UpdatePlayFieldFeedback updatePlayFieldFeedback);


        public Task<bool> DeletePlayfieldFeedback(int playFieldFeedbackId);
    }
}
