using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Repositories;
using Microsoft.Extensions.Configuration;

namespace FSU.SPORTIDY.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;



        private MeetingRepository _MeetingRepo;
        private SportidyContext _context;
        private UserRepository _UserRepo;

        public UnitOfWork(SportidyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //GenericRepository<Category> IUnitOfWork.CategoryRepository
        //{
        //    get
        //    {
        //        if (_categoryRepo == null)
        //        {
        //            this._categoryRepo = new GenericRepository<Category>(_context);
        //        }
        //        return _categoryRepo;
        //    }
        //}

        public MeetingRepository MeetingRepository
        {
            get
            {
                if (_MeetingRepo == null)
                {
                    this._MeetingRepo = new MeetingRepository(_context);
                }
                return _MeetingRepo;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (_UserRepo == null)
                {
                    this._UserRepo = new UserRepository(_context);
                }
                return _UserRepo;
            }
        }
    }
}
