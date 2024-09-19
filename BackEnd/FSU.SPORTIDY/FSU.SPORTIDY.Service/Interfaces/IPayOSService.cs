using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IPayOSService
    {
        public Task<CreatePaymentResult> createPaymentLink(long orderCode, decimal amount, string returnUrl, string cancelUrl, string description, string buyerName, string buyerPhone, string fieldName, int hour);
        public Task<PaymentLinkInformation> getPaymentLinkInformation(int orderId);

    }
}
