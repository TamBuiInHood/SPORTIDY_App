using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.BookingBsModels
{
    public class FieldTypeResponse
    {
        public int? TotalPlayField {  get; set; }
        public int? TotalBooking { get; set; }
        public List<FieldTypePercentage>? FieldPercentages { get; set; }
    }
}
