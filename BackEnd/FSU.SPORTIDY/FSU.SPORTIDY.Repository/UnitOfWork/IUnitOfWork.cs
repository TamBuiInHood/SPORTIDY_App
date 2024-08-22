using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using FSU.SPORTIDY.Repository.Repositories;

namespace FSU.SPORTIDY.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository UserRepository { get; }
        RoleRepository _RoleRepo { get; }
        UserTokenRepository _UserTokenRepo { get; }
        void Save();
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
        public MeetingRepository MeetingRepository { get; }
    }
}
