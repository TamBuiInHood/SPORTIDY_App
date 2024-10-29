using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PlayFieldsBsModels
{
    public class PlayFieldStatisticForOwner
    {
        public double? revenue {  get; set; }
        public List<BookingModel>? listBooking {  get; set; }
    }
}
