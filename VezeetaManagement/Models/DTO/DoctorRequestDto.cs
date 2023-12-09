using System.ComponentModel.DataAnnotations.Schema;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.DTO
{
    public class DoctorRequestDto
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public Days Days { get; set; }
        public DateTime DateTime { get; set; }
    }
}
