using VezeetaManagement.Models.Domain;

namespace VezeetaManagement.Repositories.Interface
{
    public interface IPatientRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();

        Task<bool> MakeBooking(TbBooking request);

        Task<IEnumerable<TbBooking>> GetAllBookings(string userId);

        Task<bool> CancelBooking(string bookingId, string userId);
    }
}
