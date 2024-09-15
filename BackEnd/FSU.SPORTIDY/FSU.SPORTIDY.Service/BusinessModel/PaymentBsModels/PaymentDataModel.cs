using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels
{
    public class PaymentDataModel
    {
        public string OrderCode { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public List<BookingModel> items { get; set; }
        public string cancelUrl { get; set; }
        public string returnUrl { get; set; }
    }
}
