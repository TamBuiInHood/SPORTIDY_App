using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface ICommentInMeetingService
    {
        Task<CommentInMeetingModel> Insert(string content, int meetingId, int userId, IFormFile Image);
        Task<CommentInMeetingModel> Update(string? content,int meetingId ,IFormFile? Image);
        Task<bool> Delete(int CommentInmeetingId);
        public Task<PageEntity<CommentInMeetingModel>> GetByMeetingId(int commentId, int PageSize, int PageIndex);
        Task<CommentInMeetingModel> GetById(int CommentId);
    }
}
