using System.ComponentModel.DataAnnotations;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.Domain
{
    public class TbDoctor 
    {
        [Key]
        public int DoctorId { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public Days Day { get; set; }
        public int SpecializationId { get; set; }
        public TbSpecialization Specialization { get; set; }
        public ICollection<TbAppointment> Appointments { get; set; }
        public ApplicationUser User { get; set; }
    }
}
