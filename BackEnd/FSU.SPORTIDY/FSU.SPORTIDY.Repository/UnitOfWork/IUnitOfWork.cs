using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using FSU.SPORTIDY.Repository.Repositories;
using FSU.SPORTIDY.Repository.Entities;

namespace FSU.SPORTIDY.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository UserRepository { get; }
        RoleRepository _RoleRepo { get; }
        UserTokenRepository _UserTokenRepo { get; }
        FriendShipRepository FriendShipRepository { get; }
        ClubRepository ClubRepository { get; }
        UserClubRepository UserClubRepository { get; }
        PlayFieldFeedbackRepository PlayFieldFeedbackRepository { get; }
        SystemFeedbackRepository SystemFeedbackRepository { get; }
        MeetingRepository MeetingRepository { get; }
        PlayFieldRepository PlayFieldRepository { get; }
        GenericRepository<Sport> SportRepository { get; }
        BookingRepository BookingRepository { get; }
        PaymentRepository PaymentRepository { get; }
        NotificationRepository NotificationRepository { get; }
        CommentInMeetingRepository CommentRepository { get; }
        UserMeetingRepository UserMeetingRepository { get; }
        ImageFeldReposiotory ImageFieldRepository { get; }

        void Save();
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();

    }
}
