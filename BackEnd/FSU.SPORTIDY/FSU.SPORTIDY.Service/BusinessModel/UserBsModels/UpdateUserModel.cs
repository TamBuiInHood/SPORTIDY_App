using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.Service.BusinessModel.UserModels
{
    public class UpdateUserModel
    {
        public int UserId { get; set; }

        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Tên là bắt buộc")]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Số điện thoại phải có 10 số")]
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }

        public string? Address { get; set; }

        public DateOnly Birthday { get; set; }

        public int? Gender { get; set; }

        public string? Avatar { get; set; }
        public int? IsDeleted { get; set; }


    }
}
