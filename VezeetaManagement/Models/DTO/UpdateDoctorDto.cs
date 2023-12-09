using System.ComponentModel.DataAnnotations.Schema;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.DTO
{
    public class UpdateDoctorDto
    {
        public decimal Price { get; set; }
        public Days Days { get; set; }
        public DateTime DateTime { get; set; }
    }
}
