using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IPaymentService
    {
        public Task<bool> Delete(int meetingID);

        public Task<PageEntity<PaymentModel>> Get(int CurrentIDLogin, string searchKey, int? PageSize, int? PageIndex);

        public Task<IEnumerable<PaymentModel>> GetAllMeetingByUserID(int userID);

        public Task<PaymentModel?> GetByID(int paymentID);

        public Task<PaymentModel?> Insert(PaymentModel EntityInsert);

        public Task<PaymentModel> Update(PaymentModel EntityUpdate);

        public Task<List<PaymentStatistic>> GetAllPaymentsAsync();
    }
}
