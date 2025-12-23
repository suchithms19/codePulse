using codePulse.API.Models.DTO;
using codePulse.API.Repositories.Implementation;
using codePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace codePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Check Email
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {
                // Check Password
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);

                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                    };

                    Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });

                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");


            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Create IdentityUser object
            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };

            // Create User
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                // Add Role to user (Reader)
                identityResult = await userManager.AddToRoleAsync(user, "Reader");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public IActionResult UserDetails()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var response = new LoginResponseDto
            {
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                Roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList()
            };

            return Ok(response);
        }

    }
}
