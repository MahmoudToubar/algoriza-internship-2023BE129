using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VezeetaManagement.Helper;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [StringLength(100, ErrorMessage = "Name must be less than 100")]
        public string? FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Name must be less than 100")]
        public string? LastName { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        [Range(50, 100000, ErrorMessage = "pelase enter price in system range")]
        public decimal Price { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [CheckAge(minAge: 18, ErrorMessage = "User must be at least 18 years old.")]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Days Days { get; set; }
        public string? AppointmentId { get; set; }
        public ICollection<TbAppointment>? Appointments { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<TbBooking> Bookings { get; set; }
        public string? SpecializationId { get; set; }
        public string? SpecializeName { get; set; }
        public TbSpecialization Specialization { get; set; }

        [Display(Name = "Age")]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth > today.AddYears(-age))
                    age--;
                return age;
            }
            set
            {

            }
        }
    }
}
