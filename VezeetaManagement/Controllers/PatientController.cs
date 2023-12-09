using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VezeetaManagement.Data;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Models.DTO;
using VezeetaManagement.Repositories.Interface;

namespace VezeetaManagement.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository patientRepository;
        private readonly VezeetaDbContext vezeetaDbContext;

        public PatientController(IPatientRepository patientRepository, VezeetaDbContext vezeetaDbContext)
        {
            this.patientRepository = patientRepository;
            this.vezeetaDbContext = vezeetaDbContext;
        }
        [HttpGet("SearchOnDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doc = await patientRepository.GetAllAsync();

            var response = new List<ApplicationUser>();
            foreach (var user in doc) 
            {
                response.Add(new ApplicationUser
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Specialization = user.Specialization,
                    Image = user.Image,
                    DateTime = user.DateTime,
                    Price = user.Price,
                    Days = user.Days,
                    Appointments = user.Appointments,                   
                });
            }
            return Ok(response);
        }


        [HttpPost("makeBooking")]
        public async Task<ActionResult<bool>> MakeBooking([FromBody] TbBookingDto request)
        {          
            var booking = new TbBooking
            {
                BookingId = request.BookingId,
                Status = request.Status,
                Day = request.Day,
                DiscountId = request.DiscountId,
                UserId = request.UserId,
            };

            var result = await patientRepository.MakeBooking(booking);

            if (result)
            {
                return Ok(new { Success = true });
            }

            return BadRequest("Unable to make booking");
        }

        [HttpGet("getAllBookings/{userId}")]
        public async Task<ActionResult<IEnumerable<TbBooking>>> GetAllBookings(string userId)
        {

            var doc = await patientRepository.GetAllBookings(userId);

            var response = new List<TbBooking>();
            foreach (var user in doc)
            {
                response.Add(new TbBooking
                {
                    BookingId=user.BookingId,
                    Status = user.Status,
                    Day = user.Day,
                    DiscountId = user.DiscountId,
                    UserId = user.UserId,
                });
            }
            return Ok(response);
        }

        [HttpPut("cancelBooking/{bookingId}")]
        public async Task<ActionResult<bool>> CancelBooking(string bookingId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var isCanceled = await patientRepository.CancelBooking(bookingId, userId);
                if (isCanceled)
                {
                    return Ok(true);
                }
                else
                {
                    return NotFound("Booking not found or already canceled");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
    }
}
