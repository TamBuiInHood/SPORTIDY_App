using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels
{
    public class ImageFieldModel
    {
        public int ImageId { get; set; }

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public int? ImageIndex { get; set; }

        public int? PlayFieldId { get; set; }

        public virtual PlayFieldModel? PlayField { get; set; }
    }
}
