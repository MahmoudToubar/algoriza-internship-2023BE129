using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.Domain
{
    public class TbBooking
    {
        [Key]
        public string BookingId { get; set; }
        public BookingStatus Status { get; set; }
        public Days Day { get; set; }
        public string DiscountId { get; set; }
        public TbDiscount Discount { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ICollection<TbAppointment> Appointments { get; set; }
        public ApplicationUser User { get; set; }
    }
}
