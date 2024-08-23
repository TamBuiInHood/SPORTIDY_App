using AutoMapper;
using FSU.SPORTIDY.Repository.Common.Enums;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public MeetingService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int meetingID)
        {
            string includeProperties = "CommentInMeetings,UserMeetings";
            var EntityDelete = await _unitOfWork.MeetingRepository.GetByCondition(x => x.MeetingId == meetingID, includeProperties: includeProperties);
            if (EntityDelete != null)
            {
                return false;
            }
            EntityDelete!.CommentInMeetings.Clear();
            EntityDelete!.UserMeetings.Clear();
            _unitOfWork.MeetingRepository.Delete(EntityDelete);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<PageEntity<MeetingModel>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex)
        {
            Expression<Func<Meeting, bool>> filter = !searchKey.IsNullOrEmpty() ? x => x.MeetingName!.Contains(searchKey, StringComparison.OrdinalIgnoreCase) : null!;

            Func<IQueryable<Meeting>, IOrderedQueryable<Meeting>> orderBy = q => q.OrderByDescending(x => (x.StartDate!.Value.Date - DateTime.Now.Date).TotalHours);
            string includeProperties = "CommentInMeetings,UserMeetings";

            var entities = await _unitOfWork.MeetingRepository
                .Get(filter: filter, orderBy: orderBy, includeProperties: includeProperties, pageIndex: PageIndex, pageSize: PageSize);
            var pagin = new PageEntity<MeetingModel>();
            pagin.List = _mapper.Map<IEnumerable<MeetingModel>>(entities).ToList();
            Expression<Func<Meeting, bool>> countMeeting = x => x.Status != (int)MeetingStatus.Deleted && x.Status != (int)MeetingStatus.Finished;
            pagin.TotalRecord = await _unitOfWork.MeetingRepository.Count(countMeeting);
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, PageSize!.Value);
            return pagin;

        }

        public async Task<IEnumerable<MeetingModel>> GetAllMeetingByUserID(int userID)
        {
            Expression<Func<Meeting, bool>> conditionGetMeeting = x => x.UserMeetings.Any(x => x.UserId == userID)  /*&& x.RoleInMeeting.Equals("host", StringComparison.OrdinalIgnoreCase)*/ ;
            Func<IQueryable<Meeting>, IOrderedQueryable<Meeting>> orderBy = x => x
    .OrderByDescending(o => o.UserMeetings.OrderBy(um => um.Meeting.StartDate).FirstOrDefault()!.Meeting.StartDate);

            string includeProperties = "UserMeetings";
            string thenIncludeProperties = "Meeting";
            var meetingOfUser = await _unitOfWork.MeetingRepository.GetAllNoPaging(filter: conditionGetMeeting,
                orderBy: orderBy,
                includeProperties: includeProperties,
                thenIncludeProperties: thenIncludeProperties);
            var mapDTO = _mapper.Map<IEnumerable<MeetingModel>>(meetingOfUser);
            return mapDTO;
        }

        public async Task<MeetingModel?> GetByID(int meetingID)
        {
            Expression<Func<Meeting, bool>> condition = x => x.MeetingId == meetingID && (x.Status != (int)MeetingStatus.Deleted);
            var entity = await _unitOfWork.MeetingRepository.GetByCondition(condition);
            return _mapper?.Map<MeetingModel?>(entity)!;

        }

        public async Task<MeetingModel?> Insert(MeetingModel EntityInsert, List<int> invitedFriend, int currentLoginID)
        {
            var userMeeting = new List<UserMeeting>();
            foreach (var player in invitedFriend)
            {
                userMeeting.Add(new UserMeeting
                {
                    UserId = player,
                    ClubId = EntityInsert.ClubId,
                    RoleInMeeting = MeetingRole.IsInvited,
                });
            }
            var meeting = new Meeting();
            meeting.MeetingCode = Guid.NewGuid().ToString();
            meeting.Address = EntityInsert.Address;
            meeting.Status = (int)MeetingStatus.Waiting;
            meeting.Host = currentLoginID;

            meeting.StartDate = EntityInsert.StartDate;
            meeting.EndDate = EntityInsert.EndDate;
            meeting.Address = EntityInsert.Address;
            meeting.SportId = EntityInsert.SportId;
            meeting.ClubId = EntityInsert.ClubId;
            meeting.CancelBefore = EntityInsert.CancelBefore;
            meeting.Note = EntityInsert.Note;
            meeting.IsPublic = EntityInsert.IsPublic;
            meeting.MeetingImage = EntityInsert.MeetingImage;
            meeting.UserMeetings = userMeeting;
            meeting.TotalMember = EntityInsert.TotalMember;
            meeting.SportId = EntityInsert.SportId;
            meeting.CancelBefore = EntityInsert.CancelBefore;

            await _unitOfWork.MeetingRepository.Insert(meeting);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result == true)
            {
                var mapdto = _mapper.Map<MeetingModel>(meeting);
                return mapdto;
            }
            return null!;
        }

        public async Task<MeetingModel> Update(MeetingModel EntityUpdate)
        {
            var meeting = await _unitOfWork.MeetingRepository.GetByCondition(x => x.MeetingId == EntityUpdate.MeetingId);
            if (meeting == null)
            {
                return null;
            }
            _mapper.Map(EntityUpdate, meeting);
            _unitOfWork.MeetingRepository.Update(meeting);
            var result = (await _unitOfWork.SaveAsync()) > 0 ? true : false ;
            if (result )
            {
                return EntityUpdate;
            }
            return null!;
        }
    }
}
