using FSU.SPORTIDY.Repository.Repositories;

namespace FSU.SPORTIDY.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task<int> SaveAsync();
        //public GenericRepository<Category> CategoryRepository { get; }
        public MeetingRepository MeetingRepository { get; }
        public  UserRepository UserRepository{ get; }
}
}
