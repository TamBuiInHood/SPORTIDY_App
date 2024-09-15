using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels
{
    public class CreatePaymentResultModel
    {
        public string bin {  get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string orderCode { get; set; }
        public string currency { get; set; }
        public string paymentLinkId { get; set; }
        public string status { get; set; }
        public string checkoutUrl { get; set; }
        public string qrCode { get; set; }
    }
}
