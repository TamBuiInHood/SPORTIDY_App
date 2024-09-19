using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.FirebaseRootFolder;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Linq.Expressions;

namespace FSU.SPORTIDY.Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int bookingId)
        {
            try
            {

                Expression<Func<Booking, bool>> condition = x => x.BookingId == bookingId;
                //var includeProperties = "Payment"
                var booking = await _unitOfWork.BookingRepository.GetByCondition(condition);
                if (booking == null)
                {
                    throw new Exception("This booking is not found");
                }
                _unitOfWork.BookingRepository.Delete(booking);
                var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PageEntity<BookingModel>> GetAll(string? searchKey, int? pageSize, int? pageIndex)
        {
            try
            {
                // Initialize filter expression
                Expression<Func<Booking, bool>> filter = x => true;

                // Try parsing searchKey as DateTime
                if (DateTime.TryParse(searchKey, out DateTime parsedDate))
                {
                    // Filter by BookingDate if searchKey is a valid DateTime
                    filter = x => x.BookingDate.HasValue && x.BookingDate.Value.Date == parsedDate.Date;
                }
                else if (!string.IsNullOrEmpty(searchKey))
                {
                    // Otherwise, filter by BookingCode or Status if searchKey is not a valid DateTime
                    filter = x => x.BookingCode!.Contains(searchKey) ||
                                  x.Status.ToString()!.Contains(searchKey);
                }

                string includeProperties = "PlayField";

                // Fetch filtered results from the repository
                var entities = await _unitOfWork.BookingRepository.Get(
                    filter: filter,
                    includeProperties: includeProperties,
                    pageSize: pageSize,
                    pageIndex: pageIndex
                );

                var pagin = new PageEntity<BookingModel>();
                pagin.List = _mapper.Map<IEnumerable<BookingModel>>(entities).ToList();

                // Count total records
                Expression<Func<Booking, bool>> countBooking = x => x.Status != (int)BookingStatusID.BOOKING_DELETED_ID;
                pagin.TotalRecord = await _unitOfWork.BookingRepository.Count(countBooking);

                pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, pageSize!.Value);
                return pagin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<BookingModel> GetById(int bookingId)
        {
            try
            {
                Expression<Func<Booking, bool>> filter = x => x.BookingId == bookingId;
                string includeProperties = "PlayField";
                var booking = await _unitOfWork.BookingRepository.GetByCondition(filter, includeProperties);
                return _mapper.Map<BookingModel>(booking);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PageEntity<BookingModel>> GetByUserId(int CurrentIDLogin, string? searchKey, int? pageSize, int? pageIndex)
        {
            try
            {
                // Initialize filter expression
                Expression<Func<Booking, bool>> filter = x => true;

                // Try parsing searchKey as DateTime
                if (DateTime.TryParse(searchKey, out DateTime parsedDate))
                {
                    // Filter by BookingDate if searchKey is a valid DateTime
                    filter = x => x.BookingDate.HasValue && x.BookingDate.Value.Date == parsedDate.Date && x.CustomerId == CurrentIDLogin;
                }
                else if (!string.IsNullOrEmpty(searchKey))
                {
                    // Otherwise, filter by BookingCode or Status if searchKey is not a valid DateTime
                    filter = x => (x.BookingCode!.Contains(searchKey) ||
                                  x.Status.ToString()!.Contains(searchKey)) && x.CustomerId == CurrentIDLogin;
                }

                string includeProperties = "PlayField";

                // Fetch filtered results from the repository
                var entities = await _unitOfWork.BookingRepository.Get(
                    filter: filter,
                    includeProperties: includeProperties,
                    pageSize: pageSize,
                    pageIndex: pageIndex
                );

                var pagin = new PageEntity<BookingModel>();
                pagin.List = _mapper.Map<IEnumerable<BookingModel>>(entities).ToList();

                // Count total records
                Expression<Func<Booking, bool>> countBooking = x => x.Status != (int)BookingStatusID.BOOKING_DELETED_ID;
                pagin.TotalRecord = await _unitOfWork.BookingRepository.Count(countBooking);

                pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, pageSize!.Value);
                return pagin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BookingModel> Insert(BookingModel entityInsert, IFormFile barCode)
        {
            try
            {
                var booking = new Booking();
                if (entityInsert.DateStart >= DateTime.Now ||
                    entityInsert.DateEnd < booking.DateStart ||
                    entityInsert.BookingDate <= DateTime.Now)
                {
                    throw new Exception("Check your booking about booking date, time starting and time endding");
                }
                _mapper.Map(entityInsert, booking);
                // upload barcode to firebase
                if (barCode != null)
                {
                    string fileName = Path.GetFileName(entityInsert.BookingCode);
                    var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                    await firebaseStorage.Child(FirebaseRoot.BOOKING_BARCODE).Child(fileName).PutAsync(barCode.OpenReadStream());
                    booking.BarCode = await firebaseStorage.Child(FirebaseRoot.BOOKING_BARCODE).Child(fileName).GetDownloadUrlAsync();
                }

                booking.PaymentMethod = entityInsert.PaymentMethod ?? "VietQR";
                booking.BookingDate = DateTime.Now;
                booking.Status = BookingStatusID.BOOKING_PENDING_ID;

                //booking.BookingCode = entityInsert.BookingCode;
                //booking.Price = entityInsert.Price;
                //booking.DateStart = entityInsert.DateStart;
                //booking.DateStart = entityInsert.DateStart;
                //booking.BarCode = entityInsert.BarCode;
                //booking.Description = entityInsert.Description;

                // add payment here

                await _unitOfWork.BookingRepository.Insert(booking);
                var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
                if (result == true)
                {
                    var mapdto = _mapper.Map<BookingModel>(booking);
                    return mapdto;
                }
                return null!;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BookingModel> Update(BookingModel entityUpdate, IFormFile barCode)
        {
            var booking = await _unitOfWork.BookingRepository.GetByCondition(x => x.BookingId == entityUpdate.BookingId);
            if (booking == null)
            {
                return null;
            }
            if (barCode != null)
            {
                // update barcode vo url co san
                var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                await firebaseStorage.Child(FirebaseRoot.BOOKING_BARCODE).Child(booking.BookingCode).PutAsync(barCode.OpenReadStream());
            }

            if (DateHelper.ValidateDates(entityUpdate.DateStart, entityUpdate.DateEnd))
            {
                booking.DateStart = entityUpdate.DateStart.Value;
                booking.DateEnd = entityUpdate.DateEnd.Value;
            }
            if (!entityUpdate.Description.IsNullOrEmpty())
            {
                booking.Description = entityUpdate.Description;
            }
            if (entityUpdate.CustomerId.HasValue)
            {
                booking.CustomerId = entityUpdate.CustomerId.Value;
            }
            _unitOfWork.BookingRepository.Update(booking);
            var result = (await _unitOfWork.SaveAsync()) > 0 ? true : false;
            if (result)
            {
                return _mapper.Map<BookingModel>(booking);
            }
            return null!;
        }

        public async Task<BookingModel> UpdateStatus(int bookingId, int status)
        {
            Expression<Func<Booking, bool>> filter = x => x.BookingId == bookingId;
            var booking = await _unitOfWork.BookingRepository.GetByID(bookingId);
            if (booking == null)
            {
                return null;
            }
            booking.Status = status;
            return _mapper.Map<BookingModel>(booking);
        }
    }
}
