using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PlayFieldFeedBackBsModels
{
    public class PlayFieldFeedbackModel
    {
        public int FeedbackId { get; set; }

        public string? FeedbackCode { get; set; }

        public string? Content { get; set; }

        public int? Rating { get; set; }

        public DateTime? FeedbackDate { get; set; }

        public int? ImageUrl { get; set; }

        public int? VideoUrl { get; set; }

        public bool? IsAnonymous { get; set; }

        public int BookingId { get; set; }

        public virtual BookingModel Booking { get; set; } = null!;
    }
}
