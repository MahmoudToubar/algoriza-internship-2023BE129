using Microsoft.EntityFrameworkCore;
using VezeetaManagement.Data;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Models.Enum;
using VezeetaManagement.Repositories.Interface;

namespace VezeetaManagement.Repositories.Implementation
{
    public class DoctorSettingRepository : IDoctorSettingRepository
    {
        private readonly VezeetaDbContext vezeetaDbContext;

        public DoctorSettingRepository(VezeetaDbContext vezeetaDbContext) 
        {
            this.vezeetaDbContext = vezeetaDbContext;
        }

        public async Task<bool> ConfirmCheckup(string bookingId, string userId)
        {
            var booking = await vezeetaDbContext.Booking
            .Include(b => b.Appointments)
            .FirstOrDefaultAsync(b => b.BookingId == bookingId && b.UserId == userId);

            if (booking == null || booking.Status != BookingStatus.Pending)
            {
                return false;
            }

            booking.Status = BookingStatus.completed;

            foreach (var appointment in booking.Appointments)
            {
                appointment.Status = BookingStatus.completed;
            }

            await vezeetaDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
        {
            await vezeetaDbContext.User.AddAsync(user);
            await vezeetaDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser?> DeleteAsync(string id)
        {
            var exist = await vezeetaDbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            if (exist != null) 
            {
                vezeetaDbContext.User.Remove(exist);
                await vezeetaDbContext.SaveChangesAsync(); 
                return exist;
            }
            return null;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await vezeetaDbContext.User.Include(x => x.Appointments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            var exist = await vezeetaDbContext.User.Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if (exist == null) 
            {
                return null;
            }

            vezeetaDbContext.Entry(exist).CurrentValues.SetValues(user);

            exist.Appointments = user.Appointments;

            await vezeetaDbContext.SaveChangesAsync();
            return user;
        }
    }
}
