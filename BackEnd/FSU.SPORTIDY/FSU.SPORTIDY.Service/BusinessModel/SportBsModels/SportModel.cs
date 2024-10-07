using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.SportBsModels
{
    public class SportModel
    {
        public int SportId { get; set; }

        public string? SportCode { get; set; }

        public string? SportName { get; set; }

        public string? SportImage { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
