using System.ComponentModel.DataAnnotations;

namespace onlineCourses.Data.ViewModels
{
    public class LoginVM
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 50 characters")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
