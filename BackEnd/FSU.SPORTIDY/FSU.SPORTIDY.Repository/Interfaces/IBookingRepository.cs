using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface IBookingRepository
    {
        public Task<List<Booking>> GetRevenuesByPlayFieldAndYearAsync(int playFieldId, int year);
        public Task<List<Booking>> GetBookingsByYearAsync(int month);
        public Task<List<Booking>> GetPlayFieldRateInBookingByYearAsync(int year);
    }
}
