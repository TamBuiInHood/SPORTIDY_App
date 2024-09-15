using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels
{
    public class PayOSKey
    {
        public string clientId { get; set; }
        public string apiKey { get; set; }
        public string checksumKey { get; set; }
        public string domain { get; set; }

    }
}
