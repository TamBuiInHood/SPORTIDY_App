using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
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
    }
}
