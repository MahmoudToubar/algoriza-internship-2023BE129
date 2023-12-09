using System.ComponentModel.DataAnnotations;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.DTO
{
    public class TbBookingDto
    {
        public string BookingId { get; set; }
        public string UserId { get; set; }
        public BookingStatus Status { get; set; }
        public Days Day { get; set; }
        public string DiscountId { get; set; }
    }
}
