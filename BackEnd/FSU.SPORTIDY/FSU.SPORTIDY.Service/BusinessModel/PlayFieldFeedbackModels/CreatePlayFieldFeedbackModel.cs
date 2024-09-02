using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PlayFieldFeedbackModels
{
    public class CreatePlayFieldFeedbackModel
    {
        public string? Content { get; set; }

        public int? Rating { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public bool? IsAnonymous { get; set; }

        public int BookingId { get; set; }
        public int PlayFieldId { get; set; }
    }
}
