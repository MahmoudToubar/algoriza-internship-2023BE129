using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VezeetaManagement.Data;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Repositories.Interface;
using VezeetaManagement.Repositories.Implementation;
using VezeetaManagement.Models.DTO;

namespace VezeetaManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;



        private readonly ITokenRepository tokenRepository;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost("register")]
        public async Task <IActionResult> Register([FromBody] UserRegistrationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                return Ok(new { Success = true });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { Message = "Invalid login attempt." });
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);

                var token = tokenRepository.CreateJwtToken(user, roles.ToList());

                var response = new LoginResponseDto()
                {
                    Email = model.Email,
                    Roles = roles.ToList(),
                    Token = token
                };
                return Ok(response);
            }

            return BadRequest(new { Message = "Invalid login attempt." });
        }
    }
}
