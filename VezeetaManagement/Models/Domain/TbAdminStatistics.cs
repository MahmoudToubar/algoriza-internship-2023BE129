using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace VezeetaManagement.Models.Domain
{
    public class TbAdminStatistics
    {
        [Key]
        public int Id { get; set; }
        public int NumOfDoctors { get; set; }
        public int NumOfPatients { get; set; }
        public List<TbSpecialization> TopSpecializations { get; set; }
        public List<TbDoctor> TopDoctors { get; set; }
        public List<ApplicationUser> User { get; set; }
    }
}
