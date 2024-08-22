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

        public Task<PageEntity<MeetingDTO>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex);
        

        public Task<IEnumerable<MeetingDTO>> GetAllMeetingByUserID(int userID);
       

        public Task<MeetingDTO?> GetByID(int meetingID);
        

        public Task<MeetingDTO?> Insert(MeetingDTO EntityInsert, List<int> invitedFriend, int currentLoginID);
       

        public Task<MeetingDTO> Update(MeetingDTO EntityUpdate);
    }
}
