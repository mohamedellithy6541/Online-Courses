using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using onlineCourses.Models.Attributes;

namespace onlineCourses.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Range(8, 25, ErrorMessage = "Age must be from 8 to 25")]
        public int Age { get; set; }
        public string? ImageURL { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public char Gender { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
