using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository 
    {
        public BookingRepository(SportidyContext context) : base(context)
        {
        }

        public async Task<List<Booking>> GetBookingsByYearAsync(int year)
        {
            return await context.Bookings
                                   .Where(b => (b.BookingDate ?? DateTime.Now).Year == year)
                                   .Include(b => b.PlayField) // Include PlayField to get field info
                                   .Include(b => b.PlayField.Sport) // Include FieldType to get field type info
                                   .ToListAsync();
        }

        public async Task<List<Booking>> GetRevenuesByPlayFieldAndYearAsync(int playFieldId, int year)
        {
            return await context.Bookings
                           .Where(r => r.PlayFieldId == playFieldId && (r.BookingDate ?? DateTime.Now).Year == year)
                           .ToListAsync();
        }

        public async Task<List<Booking>> GetPlayFieldRateInBookingByYearAsync(int year)
        {
            return await context.Bookings
                .Where(b => b.BookingDate.HasValue &&
                            b.BookingDate.Value.Year == year)
                .Include(b => b.PlayField)
                .Include(b => b.PlayField.Sport)
                .ToListAsync();
        }

    }
}
