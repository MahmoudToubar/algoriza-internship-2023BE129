using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace VezeetaManagement.Models.Domain
{
    public class TbSpecialization
    {
        [Key]
        public string SpecializationId { get; set; }
        public string SpecializeName { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
