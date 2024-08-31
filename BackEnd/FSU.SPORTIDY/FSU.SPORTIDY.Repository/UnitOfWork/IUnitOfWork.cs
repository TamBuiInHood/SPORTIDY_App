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
        MeetingRepository MeetingRepository { get; }
        PlayFieldRepository PlayFieldRepository { get; }
        GenericRepository<Sport> SportRepository { get; }
        void Save();
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();

    }
}
