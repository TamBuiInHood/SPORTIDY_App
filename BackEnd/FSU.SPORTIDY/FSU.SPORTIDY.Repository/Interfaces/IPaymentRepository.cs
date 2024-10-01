using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        public Task<List<Payment>> GetStatisticPayment();

    }
}
