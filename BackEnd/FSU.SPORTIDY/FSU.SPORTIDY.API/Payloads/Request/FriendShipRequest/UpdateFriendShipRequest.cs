using FSU.SPORTIDY.Common.Status;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.FriendShipRequest
{
    public class UpdateFriendShipRequest
    {
        [Required]
        public int currentIdLogin { get; set; }
        [Required]
        public int userId2 { get; set; }
        [Required]
        public FriendStatus status { get; set; }
    }
}
