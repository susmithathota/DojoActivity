using System.ComponentModel.DataAnnotations;
using System;
namespace DojoActivity.Models
{
    public abstract class BaseEntity {}
    public class UserViewModel: BaseEntity
    {
        [Key]
        public int UserId{get;set;}

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}