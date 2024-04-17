using System.ComponentModel.DataAnnotations;

namespace NetApplication.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Enter Name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Enter Email Address")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Enter Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!#$%^&*()-_+=]).{6,}$", ErrorMessage = "The password must contain at least one uppercase letter and be alphanumeric.")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter Mobile no")]
        [Display(Name = "Enter Mobile no")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }
    }
}
