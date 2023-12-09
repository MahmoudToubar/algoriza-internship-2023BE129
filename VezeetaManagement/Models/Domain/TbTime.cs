using System.ComponentModel.DataAnnotations;

namespace VezeetaManagement.Models.Domain
{
    public class TbTime
    {
        [Key]
        public string TimeId { get; set; }
        public DateTime DateTime { get; set; }
        public string AppointmentId { get; set; } 
        public TbAppointment Appointments { get; set; }

    }
}
