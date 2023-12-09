using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.Domain
{
    public class TbAppointment
    {
        [Key]
        public string AppointmentId { get; set; }       
        public Days Day { get; set; }
        public BookingStatus Status { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<TbTime> Times { get; set; }
        public string BookingId { get; set; }
        public TbBooking Booking { get; set; }
    }
}
