using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels;
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

        public string? BookingCode { get; set; }

        public DateTime? BookingDate { get; set; }

        public double? Price { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public int? Status { get; set; }

        public string? PaymentMethod { get; set; }

        public string? BarCode { get; set; }

        public int PlayFieldId { get; set; }

        public string? Description { get; set; }

        public int? CustomerId { get; set; }

        public string? BankCode { get; set; }
        public string? BankName { get; set; }
        public string? PlayFieldOwnerName { get; set; }
        public string? Voucher { get; set; }


        public virtual PlayFieldModel PlayField { get; set; } = null!;

        public virtual ICollection<PlayFieldFeedbackModel> PlayFieldFeedbacks { get; set; } = new List<PlayFieldFeedbackModel>();

        public virtual ICollection<PaymentModel> Payments { get; set; } = new List<PaymentModel>();

    }
}
