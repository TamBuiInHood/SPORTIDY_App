using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace FSU.SPORTIDY.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserTokenRepository UserTokenRepository { get; }
        void Save();
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
        //public GenericRepository<Category> CategoryRepository { get; }
        //public RefreshTokenRepository RefreshTokenRepository { get; }
    }
}
