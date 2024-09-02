using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.SystemFeedbackModels
{
    public class CreateSystemFeedbackModel
    {
        public string? Content { get; set; }

        public int? Rating { get; set; }

        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public bool? IsAnonymous { get; set; }
        public int UserId { get; set; }
    }
}
