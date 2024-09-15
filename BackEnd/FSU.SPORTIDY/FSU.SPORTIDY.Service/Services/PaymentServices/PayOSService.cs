using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;

namespace FSU.SPORTIDY.Service.Services.PaymentServices
{
    public class PayOSService : IPayOSService
    {
        private readonly IConfiguration _config;
        private readonly PayOSKey payOSKey;

        public PayOSService(IConfiguration config, IOptions<PayOSKey> payOSKey)
        {
            _config = config;
            this.payOSKey = payOSKey.Value;
        }

        public async Task<CreatePaymentResult> createPaymentLink(long orderCode)
        {

            PayOS payOS = new PayOS( apiKey:payOSKey.apiKey, checksumKey: payOSKey.checksumKey,clientId: payOSKey.clientId);
            ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 1000);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);

            PaymentData paymentData = new PaymentData(orderCode, 1000, "Thanh toan don hang",
                 items, "https://localhost:3002", "https://localhost:3002");

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            return createPayment;
        }

        public async Task<PaymentLinkInformation> getPaymentLinkInformation(int orderId)
        {
            PayOS payOS = new PayOS(apiKey: payOSKey.apiKey, checksumKey: payOSKey.checksumKey, clientId: payOSKey.clientId);

            PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation((long) orderId);
            return paymentLinkInformation;
        }

    }
}
