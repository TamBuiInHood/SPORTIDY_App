using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.NotificationModels
{
    public class NotificationModel
    {
        public string DeviceToken { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }

    }
}
