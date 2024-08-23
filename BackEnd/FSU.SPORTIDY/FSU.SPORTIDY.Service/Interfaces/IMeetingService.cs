using FSU.SPORTIDY.Repository.Common.Enums;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IMeetingService
    {
        public Task<bool> Delete(int meetingID);

        public Task<PageEntity<MeetingModel>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex);
        

        public Task<IEnumerable<MeetingModel>> GetAllMeetingByUserID(int userID);
       

        public Task<MeetingModel?> GetByID(int meetingID);
        

        public Task<MeetingModel?> Insert(MeetingModel EntityInsert, List<int> invitedFriend, int currentLoginID);
       

        public Task<MeetingModel> Update(MeetingModel EntityUpdate);
    }
}
