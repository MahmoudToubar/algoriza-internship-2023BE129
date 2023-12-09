using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VezeetaManagement.Data;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Models.Enum;
using VezeetaManagement.Repositories.Interface;

namespace VezeetaManagement.Repositories.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly VezeetaDbContext vezeetaDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public PatientRepository(VezeetaDbContext vezeetaDbContext,UserManager<ApplicationUser>userManager)
        {
            this.vezeetaDbContext = vezeetaDbContext;
            this.userManager = userManager;
        }

        public async Task<bool> CancelBooking(string bookingId, string userId)
        {
            var booking = await vezeetaDbContext.Booking
            .Include(b => b.Appointments)
            .FirstOrDefaultAsync(b => b.BookingId == bookingId && b.UserId == userId);

            if (booking == null || booking.Status == BookingStatus.Cancelled)
            {
                return false;
            }


            booking.Status = BookingStatus.Cancelled;

            foreach (var appointment in booking.Appointments)
            {
                appointment.Status = BookingStatus.Cancelled;
            }

            await vezeetaDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await userManager.GetUsersInRoleAsync("Doctor");
        }

        public async Task<IEnumerable<TbBooking>> GetAllBookings(string userId)
        {
            return await vezeetaDbContext.Booking.Where(b => b.UserId == userId).ToListAsync();
            
        }

        public async Task<bool> MakeBooking(TbBooking request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null || !await userManager.IsInRoleAsync(user, "Doctor"))
            {
                return false;
            }
            vezeetaDbContext.Booking.Add(request);
            await vezeetaDbContext.SaveChangesAsync();

            return true;
        }
    }
}
