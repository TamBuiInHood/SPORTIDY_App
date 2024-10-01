using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels;
using FSU.SPORTIDY.Service.BusinessModel.SportBsModels;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels
{
    public class PlayFieldModel
    {
        public int PlayFieldId { get; set; }

        public string? PlayFieldCode { get; set; }

        public string? PlayFieldName { get; set; }

        public int? Price { get; set; }

        public string? Address { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public int? UserId { get; set; }
        public string? FullName { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public string? AvatarImage { get; set; }

        public int Status { get; set; }

        public int? IsDependency { get; set; }

        public int? SportId { get; set; }
        public string? SportName { get; set; }  

        public virtual ICollection<BookingModel> Bookings { get; set; } = new List<BookingModel>();

        public virtual ICollection<ImageFieldModel> ImageFields { get; set; } = new List<ImageFieldModel>();

        public virtual UserModel? User { get; set; }

        public virtual PlayFieldModel? PlayFieldContainer { get; set; }
        public virtual ICollection<PlayFieldModel>? ListSubPlayFields { get; set; }
        public virtual SportModel? Sport { get; set; }
    }
}
