using System.ComponentModel.DataAnnotations;
using VezeetaManagement.Helper;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.DTO
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Enter Name")]
        [StringLength(100, ErrorMessage = "Name must be less than 100")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        [StringLength(100, ErrorMessage = "Name must be less than 100")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number should be 14 numbers")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone Number must be a number")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [CheckAge(minAge: 18, ErrorMessage = "User must be at least 18 years old.")]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
