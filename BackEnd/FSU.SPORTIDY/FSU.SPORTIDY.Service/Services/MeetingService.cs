﻿using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.FirebaseRootFolder;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

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
            Expression<Func<Meeting, bool>> filter = !searchKey.IsNullOrEmpty()
                                                   ? x => x.MeetingName!.Contains(searchKey, StringComparison.OrdinalIgnoreCase)
                                                   : null!;
            var includeProperties = "CommentInMeetings,UserMeetings";
            // Fetch data from the database first
            var meetings = await _unitOfWork.MeetingRepository
                .Get(filter: filter, includeProperties: includeProperties, pageIndex: PageIndex, pageSize: PageSize);

            // Switch to client-side evaluation
            var orderedMeetings = meetings.AsEnumerable()
                .OrderByDescending(x => (x.StartDate!.Value.Date - DateTime.Now.Date).TotalHours);

            var pagin = new PageEntity<MeetingModel>
            {
                List = _mapper.Map<IEnumerable<MeetingModel>>(orderedMeetings).ToList()
            };

            Expression<Func<Meeting, bool>> countMeeting = x => x.Status != (int)MeetingStatus.DELETED && x.Status != (int)MeetingStatus.FINISHED;
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
            Expression<Func<Meeting, bool>> condition = x => x.MeetingId == meetingID && (x.Status != (int)MeetingStatus.DELETED);
            string includeProperties = "UserMeetings,CommentInMeetings";
            var entity = await _unitOfWork.MeetingRepository.GetByCondition(condition,includeProperties);
            return _mapper?.Map<MeetingModel?>(entity)!;

        }

        public async Task<MeetingModel?> Insert(MeetingModel EntityInsert,  int currentLoginID, IFormFile? Image)
        {
            if (EntityInsert.StartDate < DateTime.Now || EntityInsert.EndDate < DateTime.Now)
            {
                // You can either return null or a custom response indicating invalid dates
                return null; // Or throw an exception or custom error response
            }
            var meeting = new Meeting();
            meeting.MeetingCode = Guid.NewGuid().ToString();
            meeting.Address = EntityInsert.Address;
            meeting.Status = (int)MeetingStatus.WAITING;
            meeting.Host = currentLoginID;

            meeting.StartDate = EntityInsert.StartDate;
            meeting.EndDate = EntityInsert.EndDate;
            meeting.Address = EntityInsert.Address;
            meeting.SportId = EntityInsert.SportId;
            meeting.ClubId = EntityInsert.ClubId;
            meeting.CancelBefore = EntityInsert.CancelBefore;
            meeting.Note = EntityInsert.Note;
            meeting.IsPublic = EntityInsert.IsPublic;
            //meeting.MeetingImage = EntityInsert.MeetingImage;
            //meeting.UserMeetings = userMeeting;
            meeting.TotalMember = EntityInsert.TotalMember;
            meeting.SportId = EntityInsert.SportId;
            meeting.CancelBefore = EntityInsert.CancelBefore;

            // push hinh len firebase
            if (Image != null)
            {
                string fileName = Path.GetFileName(meeting.MeetingCode);
                var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                await firebaseStorage.Child(FirebaseRoot.MEETING).Child(fileName).PutAsync(Image.OpenReadStream());
                meeting.MeetingImage = await firebaseStorage.Child(FirebaseRoot.MEETING).Child(fileName).GetDownloadUrlAsync();
            }

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

        public async Task<UserMeetingModel> UpdateRoleInMeeting(int userId, int meetingId, string RoleInMeeting)
        {
            var meeting = await _unitOfWork.UserMeetingRepository.GetByCondition(x => x.MeetingId == meetingId && x.UserId == userId);
            if (meeting == null)
            {
                return null;
            }
            meeting.RoleInMeeting = RoleInMeeting;
            _unitOfWork.UserMeetingRepository.Update(meeting);
            var result = (await _unitOfWork.SaveAsync()) > 0 ? true : false;
            if (result)
            {
                return _mapper.Map<UserMeetingModel>(meeting);
            }
            return null!;
        }

        public async Task<bool> kickUserOfMeeting(int userId, int meetingId)
        {
            var userMeeting = await _unitOfWork.UserMeetingRepository.GetByCondition(x => x.MeetingId == meetingId && x.UserId == userId);
            if (userMeeting == null)
            {
                return false;
            }
            _unitOfWork.UserMeetingRepository.Delete(userMeeting);
            var result = (await _unitOfWork.SaveAsync()) > 0 ? true : false;
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserModel>> getUsersInMeeting(int meetingId)
        {

            Expression<Func<UserMeeting, bool>> condition = x => x.MeetingId == meetingId ;
            string includeProperties = "Meeting,User";
            var userMeeting = (await _unitOfWork.UserMeetingRepository.GetAllNoPaging(filter:condition, includeProperties: includeProperties)).Select(x => x.User);

            if (!userMeeting.IsNullOrEmpty())
            {
                return null;
            }

            var mapdto = _mapper.Map<IEnumerable<UserModel>>(userMeeting);
            return mapdto;
        }

        public async Task<UserMeetingModel> insertUserMeeting(int userId, int meetingId, int? cludId)
        {

            Expression<Func<Meeting, bool>> conditionGetMeeting = x => x.MeetingId == meetingId;
            var meeting = await _unitOfWork.MeetingRepository.GetByCondition(conditionGetMeeting);
            // tham gia o qua khu
            if (meeting.StartDate < DateTime.Now || meeting.EndDate < DateTime.Now)
            {
                // You can either return null or a custom response indicating invalid dates
                return null; // Or throw an exception or custom error response
            }

            var userHasEngage = await _unitOfWork.UserMeetingRepository.GetByCondition(x => x.MeetingId == meetingId && x.UserId == userId);
            if (userHasEngage != null)
            {
                return null;
            }
            var userMeeting = new UserMeeting();
            userMeeting.UserId = userId;
            userMeeting.MeetingId = meetingId;
            if (cludId.HasValue)
            {
                userMeeting.ClubId = cludId.Value;
            }
            await _unitOfWork.UserMeetingRepository.Insert(userMeeting);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result == true)
            {
                var mapdto = _mapper.Map<UserMeetingModel>(userMeeting);
                return mapdto;
            }
            return null!;
        }
    }
}
