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
        private ClubRepository _ClubRepository;
        private UserClubRepository _UserClubRepository;
        private PlayFieldFeedbackRepository _PlayFieldFeedbackRepository;
        private SystemFeedbackRepository _SystemFeedbackRepository;
        private PlayFieldRepository _PlayFieldRepository;
        private BookingRepository _BookingRepo;
        private PaymentRepository _PaymentRepo;
        private NotificationRepository _NotficationRepo;
        private CommentInMeetingRepository _CommentRepo;
        private UserMeetingRepository _UserMeetingRepo;
        private ImageFeldReposiotory _ImageFieldRepo;
        public UnitOfWork(SportidyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _UserRepo = new UserRepository(context);
            _RoleRepo = new RoleRepository(context);
            _UserTokenRepo = new UserTokenRepository(context);
            _FriendShipRepo = new FriendShipRepository(context);
            _ClubRepository = new ClubRepository(context);
            _UserClubRepository = new UserClubRepository(context);
            _PlayFieldRepository = new PlayFieldRepository(context);
            _PlayFieldFeedbackRepository = new PlayFieldFeedbackRepository(context);
            _SystemFeedbackRepository = new SystemFeedbackRepository(context);
            _NotficationRepo = new NotificationRepository(context);
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

        public ClubRepository ClubRepository
        {
            get
            {
                if(_ClubRepository == null)
                {
                    this._ClubRepository = new ClubRepository(_context);    
                }
                return _ClubRepository;
            }
        }

        public UserClubRepository UserClubRepository
        {
            get
            {
                if(_UserClubRepository == null)
                {
                    this._UserClubRepository = new UserClubRepository(_context);
                }
                return _UserClubRepository;
            }
        }

        public PlayFieldFeedbackRepository PlayFieldFeedbackRepository
        {
            get
            {
                if (_PlayFieldFeedbackRepository == null)
                {
                    this._PlayFieldFeedbackRepository = new PlayFieldFeedbackRepository(_context);
                }
                return _PlayFieldFeedbackRepository;
            }
        }

        public SystemFeedbackRepository SystemFeedbackRepository
        {
            get
            {
                if (_SystemFeedbackRepository == null)
                {
                    this._SystemFeedbackRepository = new SystemFeedbackRepository(_context);
                }
                return _SystemFeedbackRepository;
            }
        }
        public PlayFieldRepository PlayFieldRepository
        {
            get
            {
                if (_PlayFieldRepository == null)
                {
                    this._PlayFieldRepository = new PlayFieldRepository(_context);
                }
                return _PlayFieldRepository ;
            }
        }

        public BookingRepository BookingRepository
        {
            get
            {
                if(_BookingRepo == null)
                {
                    this._BookingRepo = new BookingRepository(_context);
                }
                return _BookingRepo;
            }
        }
        public PaymentRepository PaymentRepository
        {
            get
            {
                if (_PaymentRepo == null)
                {
                    this._PaymentRepo= new PaymentRepository(_context);
                }
                return _PaymentRepo;
            }
        }

        public NotificationRepository NotificationRepository
        {
            get
            {
                if (_NotficationRepo == null)
                {
                    this._NotficationRepo = new NotificationRepository(_context);
                }
                return _NotficationRepo;
            }
        }
        public CommentInMeetingRepository CommentRepository
        {
            get
            {
                if (_CommentRepo == null)
                {
                    this._CommentRepo = new CommentInMeetingRepository(_context);
                }
                return _CommentRepo;
            }
        }
        public UserMeetingRepository UserMeetingRepository
        {
            get
            {
                if (_UserMeetingRepo == null)
                {
                    this._UserMeetingRepo = new UserMeetingRepository(_context);
                }
                return _UserMeetingRepo;
            }
        }

        public ImageFeldReposiotory ImageFieldRepository
        {
            get
            {
                if (_ImageFieldRepo == null)
                {
                    this._ImageFieldRepo = new ImageFeldReposiotory(_context);
                }
                return _ImageFieldRepo;
            }
        }
    }
}
