using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request
{
    public class AccountRequestModel
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Must be email format!")]
        [DisplayName("Email Address")]
        public string Email { get; set; } = "";

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; } = "";
    }
}
