using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager ,
            IAuthService authService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
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

            UserDto data = new(user.DisplayName, user.Email!, await _authService.CreateTokenAsync(user, _userManager));

            return Ok(data);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckEmailExist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse { Errors = new string[] {"Email is Already Exist"} });

            AppUser user = new()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
            };

            IdentityResult result = await _userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            UserDto data = new(user.DisplayName, user.Email!, await _authService.CreateTokenAsync(user, _userManager));

            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            AppUser? user = await _userManager.FindByEmailAsync(email!);

            UserDto userData = new(user!.DisplayName, user.Email!, await _authService.CreateTokenAsync(user, _userManager));

            return Ok(userData);
        }

        [Authorize]
        [HttpGet("GetUserAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            AppUser? user = await _userManager.FindUserWithAddress(User);
            AddressDto address = _mapper.Map<AddressDto>(user!.Address);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("UpdateUserAddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            UserAddress? address = _mapper.Map<AddressDto,UserAddress>(updatedAddress);
            AppUser? user = await _userManager.FindUserWithAddress(User);
            address.Id = user!.Address.Id;
            user.Address = address;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(updatedAddress);
        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
