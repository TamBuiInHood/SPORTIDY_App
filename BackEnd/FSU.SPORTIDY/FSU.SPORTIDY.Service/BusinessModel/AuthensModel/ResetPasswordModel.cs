using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FSU.SPORTIDY.Service.BusinessModel.AuthensModel
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Email is required!"), EmailAddress(ErrorMessage = "Must be email format!")]
        [Display(Name = "Email")]

        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirm Password is required!")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and password confirm does not match!")]
        public string ConfirmPassword { get; set; } = "";
    }
}
