using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using FSU.SPORTIDY.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace FSU.SPORTIDY.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private IDbContextTransaction _transaction;

        public RoleRepository _RoleRepo { get; private set; }
        public UserTokenRepository _UserTokenRepo { get; private set; }
        private MeetingRepository _MeetingRepo;
        private SportidyContext _context;
        private UserRepository _UserRepo;
        private GenericRepository<Sport> _sportRepo;
        private FriendShipRepository _FriendShipRepo;
        public UnitOfWork(SportidyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _UserRepo = new UserRepository(context);
            _RoleRepo = new RoleRepository(context);
            _UserTokenRepo = new UserTokenRepository(context);
            _FriendShipRepo = new FriendShipRepository(context);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {

            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null!;
            }
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

        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

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

        public FriendShipRepository FriendShipRepository
        {
            get
            {
                if (_FriendShipRepo == null)
                {
                    this._FriendShipRepo = new FriendShipRepository(_context);
                }
                return _FriendShipRepo;
            }
        }

        public GenericRepository<Sport> SportRepository
        {
            get
            {
                if(_sportRepo == null)
                {
                    this._sportRepo = new GenericRepository<Sport>(_context);
                }
                return _sportRepo;
            }
        }
    }
}
