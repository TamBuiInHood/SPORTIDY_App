using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IBookingService
    {
        Task<BookingModel> Insert(BookingModel entityInsert, IFormFile barCode);
        Task<BookingModel> Update(BookingModel entityUpdate, IFormFile? barCode);
        Task<bool> Delete(int bookingId);
        public Task<PageEntity<BookingModel>> GetByUserId(int CurrentIDLogin, string? searchKey, int? PageSize, int? PageIndex);
        public Task<PageEntity<BookingModel>> GetAll(string? searchKey, int? PageSize, int? PageIndex);
        Task<BookingModel> GetById(int bookingId);
        Task<BookingModel> UpdateStatus(string bookingCode, int status);
        public Task<PlayFieldRevenueResponse> GetPlayFieldRevenueAsync(int playFieldId, int year);
        public Task<PlayFieldRevenueForAdmin> GetAnnualRevenueForAdminAsync(int month);
        public Task<FieldTypeResponse> GetFieldTypePercentageAsync(int year);
    }
}
