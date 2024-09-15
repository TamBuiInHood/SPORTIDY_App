using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Service.BusinessModel.FriendShipBSModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IFriendShipService
    {
        public Task<bool> Delete(int friendshipId);

        public Task<PageEntity<FriendShipModel>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex);

        public Task<IEnumerable<FriendShipModel>> GetAllFrinedOfUserID(int CurrentIDLogin, string searchKey);

        public Task<FriendShipModel?> GetBy2ID(int userId1, int userId2);

        public Task<FriendShipModel?> Insert(int currentLoginID, int UserID2);

        public Task<FriendShipModel> updateStatus(int currentIDLogin, int userId2, int status);
        public Task<FriendShipModel?> GetByID(int frineshipId);
    }
}
