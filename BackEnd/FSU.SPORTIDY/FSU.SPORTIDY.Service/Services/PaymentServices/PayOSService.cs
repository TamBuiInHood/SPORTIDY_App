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
        private readonly PayOSKey _payOSKey;

        public PayOSService(IConfiguration config, IOptions<PayOSKey> payOSKey)
        {
            _config = config;
            this._payOSKey = payOSKey.Value;
        }

        public async Task<CreatePaymentResult> createPaymentLink(long orderCode, decimal amount, string returnUrl, string cancelUrl, string description, string buyerName, string buyerPhone, string fieldName, int hour)
        {
            var domain = _payOSKey.domain;
            PayOS payOS = new PayOS(apiKey: _payOSKey.apiKey, checksumKey: _payOSKey.checksumKey, clientId: _payOSKey.clientId);
            ItemData item = new ItemData(fieldName, hour,(int) amount);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);

            PaymentData paymentData = new PaymentData(
                orderCode: orderCode,
                amount: (int)amount,
                description: description,
                items: items,
                buyerName: buyerName,
                buyerPhone: buyerPhone,
                cancelUrl: $"{domain}/{cancelUrl}",
                returnUrl: $"{domain}/{returnUrl}"
                );

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            return createPayment;
        }

        public async Task<PaymentLinkInformation> getPaymentLinkInformation(int orderId)
        {
            PayOS payOS = new PayOS(apiKey: _payOSKey.apiKey, checksumKey: _payOSKey.checksumKey, clientId: _payOSKey.clientId);

            PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation((long) orderId);
            return paymentLinkInformation;
        }

    }
}
