using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.NotificationModels
{
    public class NotificationListModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public List<int>? ListUserId { get; set; }
    }
}
