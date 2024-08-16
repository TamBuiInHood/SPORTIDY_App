namespace FSU.SPORTIDY.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task<int> SaveAsync();
        //public GenericRepository<Category> CategoryRepository { get; }
        //public RefreshTokenRepository RefreshTokenRepository { get; }
    }
}
