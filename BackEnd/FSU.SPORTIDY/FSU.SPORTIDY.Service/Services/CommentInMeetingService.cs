using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.FirebaseRootFolder;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Services
{
    public class CommentInMeetingService : ICommentInMeetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly WebSocketService _websocketService;

        public CommentInMeetingService(IUnitOfWork unitOfWork, IMapper mapper, WebSocketService websocketService)
        {
            _unitOfWork = unitOfWork;
            this._mapper = mapper;
            _websocketService = websocketService;
        }

        //public WebSocketService WebsocketService => _websocketService;

        public async Task<bool> Delete(int CommentInmeetingId)
        {
            var comment = await _unitOfWork.CommentRepository.GetByID(CommentInmeetingId);
            if (comment == null)
            {
                throw new Exception("Can not found this comment");
            }
            _unitOfWork.CommentRepository.Delete(comment);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }

        public async Task<CommentInMeetingModel> GetById(int CommentId)
        {
            Expression<Func<CommentInMeeting, bool>> filter = x => x.CommentId == CommentId;
            var commnet = await _unitOfWork.CommentRepository.GetByCondition(filter);
            var mapdto = _mapper.Map<CommentInMeetingModel>(commnet);
            return mapdto;
        }

        public async Task<PageEntity<CommentInMeetingModel>> GetByMeetingId(int meetingId, int PageSize, int PageIndex)
        {
            Expression<Func<CommentInMeeting, bool>> filter = x => x.MeetingId == meetingId;
            var allComment = await _unitOfWork.CommentRepository.Get(filter, pageSize: PageSize, pageIndex: PageIndex);
            allComment.OrderByDescending(x => x.CommentDate!.Value.TimeOfDay);

            var pagin = new PageEntity<CommentInMeetingModel>();

            pagin.List = _mapper.Map<IEnumerable<CommentInMeetingModel>>(allComment);
            pagin.TotalRecord = await _unitOfWork.CommentRepository.Count(filter);
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, PageSize);
            return pagin;
        }

        public async Task<CommentInMeetingModel> Insert(string content, int meetingId, int userId, IFormFile Image)
        {
            var comment = new CommentInMeeting();
            comment.Content = content;
            comment.MeetingId = meetingId;
            comment.UserId = userId;
            comment.CommentDate = DateTime.Now;
            comment.CommentCode = Guid.NewGuid().ToString();
            if (Image != null)
            {
                string fileName = Path.GetFileName(comment.CommentCode);
                var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                await firebaseStorage.Child($"{FirebaseRoot.COMMENT}/{meetingId}").Child(comment.CommentCode).PutAsync(Image.OpenReadStream());
                comment.Image = await firebaseStorage.Child($"{FirebaseRoot.COMMENT}/{meetingId}").Child(fileName).GetDownloadUrlAsync();
            }
            await _unitOfWork.CommentRepository.Insert(comment);

            var result = await _unitOfWork.SaveAsync() > 0;
            if (result)
            {
                var mappedComment = _mapper.Map<CommentInMeetingModel>(comment);

                // Broadcast the new comment to all WebSocket clients
                await _websocketService.BroadcastNewComment(mappedComment);
                return mappedComment;
            }
            return null!;
        }

        public async Task<CommentInMeetingModel> Update(string? content, int commentId, IFormFile? Image)
        {
            Expression<Func<CommentInMeeting, bool>> filter = x => x.CommentId == commentId;
            var comment = await _unitOfWork.CommentRepository.GetByCondition(filter);

            if (comment == null)
            {
                throw new Exception("Not found comment");
            }

            if (Image != null)
            {
                var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                await firebaseStorage.Child($"{FirebaseRoot.COMMENT}/{comment.MeetingId}").Child(comment.CommentCode).PutAsync(Image.OpenReadStream());
            }
            if (!content.IsNullOrEmpty())
            {
                comment.Content = content;
            }

            _unitOfWork.CommentRepository.Update(comment);

            var result = await _unitOfWork.SaveAsync() > 0;
            if (result)
            {
                return _mapper.Map<CommentInMeetingModel>(comment);
            }
            return null!;
        }

    }
}
