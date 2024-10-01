using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.NotificationModels
{
    public class NotificationListModel
    {
        public string title { get; set; }
        public string message { get; set; }

        public List<int>? listUserId { get; set; }
    }
}
