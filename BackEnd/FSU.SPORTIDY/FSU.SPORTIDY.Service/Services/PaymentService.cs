using AutoMapper;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels;
using FSU.SPORTIDY.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<bool> Delete(int meetingID)
        {
            throw new NotImplementedException();
        }

        public Task<PageEntity<PaymentModel>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentModel>> GetAllMeetingByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PaymentStatistic>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.PaymentRepository.GetStatisticPayment();
            var paymentInfoList = new List<PaymentStatistic>();

            foreach (var payment in payments)
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(payment.Booking.CustomerId.Value);
                paymentInfoList.Add(new PaymentStatistic
                {
                    Email = user?.Email, 
                    DateOfTransaction = payment.DateOfTransaction,
                    TotalAmount = payment.Booking.Price,
                    Status = payment.Status.HasValue ? (PaymentStatus?)payment.Status.Value : null
                });
            }

            return paymentInfoList;
        }

        public Task<PaymentModel?> GetByID(int paymentID)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentModel?> Insert(PaymentModel EntityInsert)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentModel> Update(PaymentModel EntityUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
