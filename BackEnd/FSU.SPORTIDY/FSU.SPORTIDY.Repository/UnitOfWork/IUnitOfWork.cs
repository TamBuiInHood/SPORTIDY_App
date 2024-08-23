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
        void Save();
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
        public MeetingRepository MeetingRepository { get; }

        public GenericRepository<Sport> SportRepository { get; }
    }
}
