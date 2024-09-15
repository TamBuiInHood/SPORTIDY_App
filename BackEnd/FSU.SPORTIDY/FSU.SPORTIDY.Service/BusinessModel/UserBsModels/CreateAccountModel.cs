using FSU.SPORTIDY.Common.Role;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.Service.BusinessModel.UserModels
{
    public class CreateAccountModel
    {
        [Required(ErrorMessage = "Email is required!"), EmailAddress(ErrorMessage = "Must be email format!")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Full name is required!")]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = null!;

        public int Gender { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? AvatarUrl { get; set; }

        public UserRoleEnum Role { get; set; }
    }
}
