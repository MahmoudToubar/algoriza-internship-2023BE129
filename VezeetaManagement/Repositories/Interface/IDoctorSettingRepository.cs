using VezeetaManagement.Models.Domain;

namespace VezeetaManagement.Repositories.Interface
{
    public interface IDoctorSettingRepository
    {
        Task<ApplicationUser> CreateAsync(ApplicationUser user);

        Task<ApplicationUser?> GetByIdAsync(string id);

        Task<ApplicationUser?> UpdateAsync(ApplicationUser user);

        Task<ApplicationUser?> DeleteAsync(string id);

        Task<bool> ConfirmCheckup(string bookingId, string userId);
    }
}
