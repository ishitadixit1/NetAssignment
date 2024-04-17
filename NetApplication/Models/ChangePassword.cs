using System.ComponentModel.DataAnnotations;

namespace NetApplication.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please enter current password")]
        [Display(Name = "Enter current password")]
        public string? currentPassword { get; set; }

        [Required(ErrorMessage = "Please enter new password")]
        [Display(Name = "Enter new password")]
        public string? newPassword { get; set; }

    }
}
