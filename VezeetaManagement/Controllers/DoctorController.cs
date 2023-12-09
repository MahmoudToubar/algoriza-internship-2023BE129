using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Models.DTO;
using VezeetaManagement.Repositories.Interface;

namespace VezeetaManagement.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorSettingRepository doctorSettingRepository;

        public DoctorController(IDoctorSettingRepository doctorSettingRepository)
        {
            this.doctorSettingRepository = doctorSettingRepository;
        }
        [HttpPost("AddAppointment")]
        public async Task<IActionResult> CreateDoctorSetting([FromBody] DoctorRequestDto request)
        {
            var user = new ApplicationUser
            {
                Price = request.Price,
                Days = request.Days,
                DateTime = request.DateTime,
            };

            user = await doctorSettingRepository.CreateAsync(user);

            var response = new DoctorResponseDto
            {
                UserId = user.Id,
                Price = user.Price,
                Days = user.Days,
                DateTime = user.DateTime,
            };
            return Ok(new { Success = true });
        }

        [HttpGet]
        [Route("GetDoctorById {id:Guid}")]
        public async Task<IActionResult> GetDoctorById([FromRoute] string id)
        {
           var doctor = await doctorSettingRepository.GetByIdAsync(id);

           if (doctor is  null) 
            {
                return NotFound();
            }

            var response = new DoctorResponseDto
            {
                UserId = doctor.Id,
                Price = doctor.Price,
                Days = doctor.Days,
                DateTime = doctor.DateTime,
            };

            return Ok(new { Success = true });
        }


        [HttpPut]
        [Route("UpdateAppointment {id:Guid}")]
        public async Task<IActionResult> UpdateDoctorSettingById([FromRoute] string id, UpdateDoctorDto request)
        {
            
            var user = new ApplicationUser
            {
                Id = id,
                Price = request.Price,
                Days = request.Days,
                DateTime = request.DateTime,
            };          

            var updated = await doctorSettingRepository.UpdateAsync(user);

            if (updated == null)
            {
                return NotFound();
            }
          
            return Ok(new { Success = true });
        }

        [HttpDelete]
        [Route("DeleteAppointment{id:Guid}")]

        public async Task<IActionResult> DeleteDoctorSetting([FromRoute] string id)
        {
            var delete = await doctorSettingRepository.DeleteAsync(id);
            if (delete == null) 
            {
                return NotFound();
            }

            var user = new ApplicationUser
            {
                Id = delete.Id,
                Price = delete.Price,
                Days = delete.Days,
                DateTime = delete.DateTime,
            };

            return Ok(new { Success = true });

        }


        [HttpPut("ConfirmCheckup")]
        public async Task<ActionResult<bool>> ConfirmCheckup(string bookingId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var isConfirmed = await doctorSettingRepository.ConfirmCheckup(bookingId, userId);
                if (isConfirmed)
                {
                    return Ok(true);
                }
                else
                {
                    return NotFound("Booking not found or not in a pending state");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

    }
}
