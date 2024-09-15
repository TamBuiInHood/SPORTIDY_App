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
        public Task<CreatePaymentResult> createPaymentLink(long orderCode);
        public Task<PaymentLinkInformation> getPaymentLinkInformation(int orderId);

    }
}
