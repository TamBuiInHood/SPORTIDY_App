using AutoMapper;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.FriendShipBSModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FSU.SPORTIDY.Service.Services
{
    public class FriendShipService : IFriendShipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FriendShipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int friendshipId)
        {
            var friendship = await _unitOfWork.FriendShipRepository.GetByCondition(x => x.FriendShipId == friendshipId);
            if (friendship == null)
            {
                throw new Exception("Can not delete this friend ship");
            }
            _unitOfWork.FriendShipRepository.Delete(friendship);
            var result = (await _unitOfWork.SaveAsync()) > 0 ? true : false;
            return result;
        }

        public async Task<PageEntity<FriendShipModel>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex)
        {
            Expression<Func<Friendship, bool>> filter1 = !searchKey.IsNullOrEmpty() ? x => x.UserId1 == CurrentIDLogin && x.UserId2Navigation.FullName.ToLower().Contains(searchKey.ToLower()) && x.Status == (int) FriendStatus.FRIEND : x => x.UserId1 == CurrentIDLogin && x.Status == (int)FriendStatus.FRIEND;
            Expression<Func<Friendship, bool>> filter2 = !searchKey.IsNullOrEmpty() ? x => x.UserId2 == CurrentIDLogin && x.UserId1Navigation.FullName.ToLower().Contains(searchKey.ToLower()) : x => x.UserId2 == CurrentIDLogin && x.Status == (int)FriendStatus.FRIEND;

            Func<IQueryable<Friendship>, IOrderedQueryable<Friendship>> orderBy = q => q.OrderByDescending(x => x.UserId2Navigation.FullName);
            string includeProperties = "UserId1Navigation,UserId2Navigation";

            var entities1 = await _unitOfWork.FriendShipRepository
                .Get(filter: filter1, orderBy: orderBy, includeProperties: includeProperties, pageIndex: PageIndex, pageSize: PageSize);
            var entities2 = await _unitOfWork.FriendShipRepository
                .Get(filter: filter2, orderBy: orderBy, includeProperties: includeProperties, pageIndex: PageIndex, pageSize: PageSize);
            var friendList = entities1.Union(entities2);
            var pagin = new PageEntity<FriendShipModel>();
            pagin.List = _mapper.Map<IEnumerable<FriendShipModel>>(friendList).ToList();
            Expression<Func<Friendship, bool>> countMeeting = x => x.UserId1 == CurrentIDLogin;
            pagin.TotalRecord = await _unitOfWork.FriendShipRepository.Count(countMeeting);
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, PageSize!.Value);
            return pagin;
        }

        public async Task<IEnumerable<FriendShipModel>> GetAllFrinedOfUserID(int CurrentIDLogin, string searchKey)
        {
            Expression<Func<Friendship, bool>> filter1 = !searchKey.IsNullOrEmpty() ? x => x.UserId1 == CurrentIDLogin && x.UserId2Navigation.FullName.ToLower().Contains(searchKey.ToLower()) && x.Status == (int)FriendStatus.FRIEND : x => x.UserId1 == CurrentIDLogin && x.Status == (int)FriendStatus.FRIEND;
            Expression<Func<Friendship, bool>> filter2 = !searchKey.IsNullOrEmpty() ? x => x.UserId2 == CurrentIDLogin && x.UserId1Navigation.FullName.ToLower().Contains(searchKey.ToLower()) : x => x.UserId2 == CurrentIDLogin && x.Status == (int)FriendStatus.FRIEND;
            var listFriend1 = await _unitOfWork.FriendShipRepository.GetAllNoPaging(filter1);
            var listFriend2 = await _unitOfWork.FriendShipRepository.GetAllNoPaging(filter2);
            var listFriend = listFriend1.Union(listFriend2);
            var mapdto = _mapper.Map<IEnumerable<FriendShipModel>>(listFriend);
            return mapdto;

        }

        public async Task<FriendShipModel?> GetBy2ID(int userId1, int userId2)
        {
            Expression<Func<Friendship, bool>> condition = x => (x.UserId1 == userId1 && x.UserId2 == userId2 && x.Status == (int)FriendStatus.FRIEND) || ( x.UserId1 == userId2 && x.UserId2 == userId1 && x.Status == (int)FriendStatus.FRIEND);
            var entity = await _unitOfWork.FriendShipRepository.GetByCondition(condition);
            return _mapper?.Map<FriendShipModel?>(entity)!;
        }
        public async Task<FriendShipModel?> GetByID(int frineshipId)
        {
            Expression<Func<Friendship, bool>> condition = x => x.FriendShipId == frineshipId && x.Status == (int)FriendStatus.FRIEND ;
            var entity = await _unitOfWork.FriendShipRepository.GetByCondition(condition);
            return _mapper?.Map<FriendShipModel?>(entity)!;
        }
        public async Task<FriendShipModel?> Insert(int currentLoginID, int UserID2)
        {
            var friendship = new Friendship();
            friendship.UserId1 = currentLoginID;
            friendship.UserId2 = UserID2;
            friendship.FriendShipCode = Guid.NewGuid().ToString();
            friendship.Status = (int)FriendStatus.FRIEND;
            friendship.CreateDate = DateOnly.FromDateTime(DateTime.Now);
            friendship.RequestBy = currentLoginID;

            await _unitOfWork.FriendShipRepository.Insert(friendship);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result == true)
            {
                var mapdto = _mapper.Map<FriendShipModel>(friendship);
                return mapdto;
            }
            return null!;
        }

        public async Task<FriendShipModel> updateStatus(int currentIDLogin, int userId2, int status)
        {
            Expression<Func<Friendship, bool>> condition = x => ( x.UserId1 == currentIDLogin && x.UserId2 == userId2 ) || ( x.UserId1 == userId2 && x.UserId2 == currentIDLogin);
            var entityUpdate = await _unitOfWork.FriendShipRepository.GetByCondition(condition);
            if (entityUpdate == null)
            {
                throw new Exception("Can not find this friend");
            }
            entityUpdate.Status = status;
            _unitOfWork.FriendShipRepository.Update(entityUpdate);
            var result = (await _unitOfWork.SaveAsync()) > 0 ? true : false;
            if (result)
            {
                return _mapper.Map<FriendShipModel>(entityUpdate);
            }
            return null!;
        }
    }
}
