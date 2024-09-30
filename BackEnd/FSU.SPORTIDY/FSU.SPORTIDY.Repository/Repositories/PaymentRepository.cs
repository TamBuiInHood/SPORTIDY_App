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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(SportidyContext context) : base(context)
        {
        }

        public async Task<List<Payment>> GetStatisticPayment()
        {
            return await context.Payments.Include(x => x.Booking).ToListAsync();
        }
    }
}
