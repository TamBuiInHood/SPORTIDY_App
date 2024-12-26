using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.NotificationModels
{
    public class NotificationModel
    {
        public string deviceToken { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public int userId { get; set; }

    }
}
