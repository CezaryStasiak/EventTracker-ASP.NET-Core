using System.ComponentModel.DataAnnotations;

namespace EventTracker.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(50, ErrorMessage = "Your email is a bit too long!")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string UserEmail { get; set; }
        
        [Required]
        [MaxLength(50, ErrorMessage = "Your password is a bit too long!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"\w+", ErrorMessage = "Password can only contain letters and numbers!")]
        public string UserPassword { get; set; }
    }
}
