using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Models.Domain
{
    public class TbDiscount
    {
        [Key]
        public string DiscountId { get; set; }
        public string Coupon { get; set; }
        public DiscountType Type { get; set; }
        public decimal Price { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ICollection<TbBooking> Bookings { get; set; }
    }
}
