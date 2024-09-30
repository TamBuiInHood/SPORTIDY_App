using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.FriendShipRequest
{
    public class AddFriendRequest
    {
        [Required]
        public int currentIdLogin { get; set; }
        [Required]
        public int userId2 { get; set; }
    }
}
