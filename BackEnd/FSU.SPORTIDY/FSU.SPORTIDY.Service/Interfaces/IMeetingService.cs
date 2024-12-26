using FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using Microsoft.AspNetCore.Http;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IMeetingService
    {
        public Task<bool> Delete(int meetingID);

        public Task<PageEntity<MeetingModel>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex);
        
        public Task<IEnumerable<MeetingModel>> GetAllMeetingByUserID(int userID);
       
        public Task<MeetingModel?> GetByID(int meetingID);
        
        public Task<MeetingModel?> Insert(MeetingModel EntityInsert, int currentLoginID, IFormFile? Image);
       
        public Task<MeetingModel> Update(MeetingModel EntityUpdate);

        public Task<UserMeetingModel> UpdateRoleInMeeting(int userId, int meetingId, string RoleInMeeting);

        public Task<bool> kickUserOfMeeting(int userId, int meetingId);

        public Task<UserMeetingModel> insertUserMeeting(int userId, int meetingId);

        public Task<IEnumerable<UserModel>> getUsersInMeeting(int meetingId);
    }
}
