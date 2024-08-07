using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            AppUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);

            if (!result.Succeeded) 
                return Unauthorized(new ApiResponse(401));

            UserDto data = new()
            {
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = "Token"
            };

            return Ok(data);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            AppUser user = new()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
            };

            IdentityResult result = await _userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            UserDto data = new()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Token"
            };

            return Ok(data);
        }
    }
}
