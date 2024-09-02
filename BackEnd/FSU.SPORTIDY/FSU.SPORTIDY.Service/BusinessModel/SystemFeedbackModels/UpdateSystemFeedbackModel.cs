using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.SystemFeedbackModels
{
    public class UpdateSystemFeedbackModel
    {
        public int FeedbackId { get; set; }

        public string? FeedbackCode { get; set; }

        public string? Content { get; set; }

        public int? Rating { get; set; }

        public DateTime? FeedbackDate { get; set; }

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public bool? IsAnonymous { get; set; }
    }
}
