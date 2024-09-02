using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.SystemFeedbackModels
{
    public class SystemFeedbackDashBoard
    {
        public int? TotalFeedback {  get; set; }
        public int? TotalImage {  get; set; }
        public int? TotalVideo {  get; set; }
        public int? TotalRating {  get; set; }
    }
}
