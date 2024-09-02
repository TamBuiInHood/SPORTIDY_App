﻿using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldFeedBackBsModels;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.BookingBsModels
{
    public class BookingModel
    {
        public int BookingId { get; set; }

        public int? BookingCode { get; set; }

        public DateTime? BookingDate { get; set; }

        public int? Price { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public int? Status { get; set; }

        public int? PaymentMethod { get; set; }

        public int? BarCode { get; set; }

        public int PlayFieldId { get; set; }

        public string? Description { get; set; }

        public int? CustomerId { get; set; }

        public virtual PlayFieldModel PlayField { get; set; } = null!;

        public virtual ICollection<PlayFieldFeedbackModel> PlayFieldFeedbacks { get; set; } = new List<PlayFieldFeedbackModel>();
    }
}