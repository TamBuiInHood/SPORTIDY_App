using FSU.SPORTIDY.Service.Models.Pagination;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.Models.MeetingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.ISerivice
{
    public interface IMeetingService
    {
        Task<PageEntity<MeetingDTO>> Get(int CurrentIDLogin,
            string searchKey,
            int? PageSize = null,
            int? PageIndex = null);
        
        Task<MeetingDTO?> GetByID(int ID);

        Task<MeetingDTO?> Insert(MeetingDTO EntityInsert, List<int> invitedFriend, int currentLoginID);

        Task<bool> Delete(int ID);
        Task<MeetingDTO> Update(MeetingDTO EntityUpdate);
        Task<IEnumerable<MeetingDTO>> GetAllMeetingByUserID(int userID);
    }
}
