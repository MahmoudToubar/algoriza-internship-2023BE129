using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VezeetaManagement.Data;
using VezeetaManagement.Models;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Models.DTO;
using VezeetaManagement.Models.Enum;

namespace VezeetaManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminStatisticsController : ControllerBase
    {
        private readonly VezeetaDbContext vezeetaDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminStatisticsController(VezeetaDbContext vezeetaDbContext, UserManager<ApplicationUser> userManager)
        {
            this.vezeetaDbContext = vezeetaDbContext;
            this.userManager = userManager;
        }

        [HttpGet("NumOfDoctors")]
        public async Task<IActionResult> GetNumberOfDoctors()
        {
            try
            {
                var doctors = await userManager.GetUsersInRoleAsync("Doctor");
                var doctorCount = doctors.Count-1;

                return Ok(new { NumberOfDoctors = doctorCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("NumOfPatients")]
        public async Task<IActionResult> GetNumberOfPatients()
        {
            try
            {
                var patients = await userManager.GetUsersInRoleAsync("User");
                var patientCount = patients.Count-1;

                return Ok(new { NumberOfPatients = patientCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorRegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Doctor");

                    await userManager.AddClaimAsync(user, new Claim("Specialization", model.SpecializeName));

                    return Ok(new { Success = true });
                }

                return BadRequest(new { Message = "Failed to create doctor account.", Errors = result.Errors });
            }

            return BadRequest(new { Message = "Invalid model state." });
        }

        [HttpGet("NumOfRequests")]
        public IActionResult GetRequestStatistics()
        {
            try
            {
                var totalRequests = vezeetaDbContext.Booking.Count();
                var pendingRequests = vezeetaDbContext.Booking.Count(r => r.Status == BookingStatus.Pending);
                var completedRequests = vezeetaDbContext.Booking.Count(r => r.Status == BookingStatus.completed);
                var cancelledRequests = vezeetaDbContext.Booking.Count(r => r.Status == BookingStatus.Cancelled);

                var result = new
                {
                    TotalRequests = totalRequests,
                    PendingRequests = pendingRequests,
                    CompletedRequests = completedRequests,
                    CancelledRequests = cancelledRequests
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("Top5-Specializations")]
        public IActionResult GetTop5Specializations()
        {
            try
            {
                var topSpecializations = vezeetaDbContext.Booking
                    .GroupBy(b => b.User.Specialization.SpecializeName)
                    .Select(group => new
                    {
                        SpecializationName = group.Key,
                        Requests = group.Count()
                    })
                    .OrderByDescending(s => s.Requests)
                    .Take(5)
                    .ToList();

                return Ok(topSpecializations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("Top10-Doctors")]
        public IActionResult GetTop10Doctors()
        {
            try
            {
                var topDoctors = vezeetaDbContext.Users
                    .Where(u => userManager.IsInRoleAsync(u, "Doctor").Result)
                    .GroupJoin(
                        vezeetaDbContext.Booking,
                        user => user.Id,
                        booking => booking.UserId,
                        (user, bookings) => new
                        {
                            user.Id,
                            user.FirstName,
                            user.LastName,
                            user.Image,
                            user.Specialization.SpecializeName,
                            Requests = bookings.Count()
                        })
                    .OrderByDescending(d => d.Requests)
                    .Take(10)
                    .ToList();

                return Ok(topDoctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
