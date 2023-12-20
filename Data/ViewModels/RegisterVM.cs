using Microsoft.EntityFrameworkCore.Metadata.Internal;
using onlineCourses.Models.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace onlineCourses.Data.ViewModels
{
    public class RegisterVM
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 50 characters")]
        public string UserName { get; set; }
        [Required]
        [Range(8, 25, ErrorMessage = "Age must be from 8 to 25")]
        public int Age { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address length must be from 5 to 100 characters")]
        public string Address { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Password Must Be Atleast 10 Characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string? ImageURL { get; set; }
    }
}
