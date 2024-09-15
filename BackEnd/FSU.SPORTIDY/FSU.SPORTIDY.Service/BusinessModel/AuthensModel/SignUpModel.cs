using FSU.SPORTIDY.Common.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.AuthensModel
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Email is required!"), EmailAddress(ErrorMessage = "Must be email format!")]
        [Display(Name = "Email address")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "Full name is required!")]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = "";
        [Required(ErrorMessage = "Confirm Password is required!")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = "";
        public string Avatar { get; set; } = "";

        public UserRoleEnum Role { get; set; }
    }
}
