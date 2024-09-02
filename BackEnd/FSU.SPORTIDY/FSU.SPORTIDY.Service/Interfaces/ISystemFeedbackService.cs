using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldFeedbackModels;
using FSU.SPORTIDY.Service.BusinessModel.SystemFeedbackModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface ISystemFeedbackService
    {
        public Task<SystemFeedbackModel> CreateFeedback(CreateSystemFeedbackModel model);
        public Task<string> UploadImageSystemFeedback(IFormFile imageSystemFeedback);
        public Task<string> UploadVideoSystenFeedback(IFormFile videoSystemFeedback);

        public Task<PageEntity<SystemFeedbackModel>> GetAllSystemFeedback(PaginationParameter paginationParameter);
        public Task<SystemFeedbackModel> GetSystemFeedbackById(int feedbackId);
        public Task<List<SystemFeedbackModel>> GetSystemFeedbackByUserId(int userId);
        public Task<bool> UpdateSystemFeedback(UpdateSystemFeedbackModel updateSystemFeedback);


        public Task<bool> DeleteSystemFeedback(int systemFeedbackId);

        public Task<SystemFeedbackDashBoard> GetSystemFeedbackDashBoard();
        public Task<PageEntity<SystemFeedbackModel>> GetAllSystemFeedbackWithNoPaging();
    }
}
