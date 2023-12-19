using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.IdentityServer.Dtos;
using PhoneBook.IdentityServer.Models;
using PhoneBook.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace PhoneBook.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Title = registerDto.Title,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Shared.Dtos.Response<TNoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return BadRequest();
            return Ok(new { Id = user.Id, UserName = user.UserName, EMail = user.Email, Title = user.Title });
        }
    }
}
