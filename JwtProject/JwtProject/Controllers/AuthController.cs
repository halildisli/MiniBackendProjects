using JwtProject.Models;
using JwtProject.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public AuthController(IJwtTokenProvider jwtTokenProvider)
        {
            _jwtTokenProvider = jwtTokenProvider;
        }
        //FromServices ne işe yarar? --->  Bu controller içerisinde çok az kullanacağımız serviceleri constructor da enjekte edip kalabalık yaratmaması adına yapılan bir işlem. Constructor da paramatre olarak alıp enjeksiyon yapmakla bir farkı yok.
        [HttpPost]
        public async Task<IActionResult> Register([FromServices] UserManager<AppUser> userManager, RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registrationDto);
            }
            var user = new AppUser
            {
                Email = registrationDto.Email,
                EmailConfirmed = true,
                UserName = registrationDto.Email,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName
            };

            var result = await userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new AuthResult
                {
                    IsSuccess = false,
                    Message = result.ToString()
                });
            }
            var token = _jwtTokenProvider.GenerateToken(user);

            return Ok(new AuthResult
            {
                IsSuccess = true,
                Token = token
            });

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices]SignInManager<AppUser> signInManager, LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await signInManager.UserManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                return BadRequest(new AuthResult
                {
                    IsSuccess = false,
                    Message = "Invalid Request"
                });
            }
            var signInResult=await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!signInResult.Succeeded)
            {
                return BadRequest(new AuthResult
                {
                    IsSuccess = false,
                    Message = "Invalid Request"
                });
            }
            var token = _jwtTokenProvider.GenerateToken(user);
            return Ok(new AuthResult
            {
                IsSuccess = true,
                Token = token
            });
        }

    }
}
