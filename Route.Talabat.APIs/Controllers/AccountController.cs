using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Application.Auth;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Services.Contract;

namespace Route.Talabat.APIs.Controllers
{

	public class AccountController : _BaseController
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAuthService _authService;
		private readonly UserManager<ApplicationUser>	_userManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager , IAuthService authService)
		{
			_signInManager = signInManager;
			this._authService = authService;
			_userManager = userManager;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);

			if (user is  null)
				return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized,"Invalid login"));
			
			var isPasswordCorrect = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

			if (!isPasswordCorrect.Succeeded)
				return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, "Invalid login"));

			return Ok(new UserDto() { 
				Email = user.Email ?? "Empty email",
				DisplayName = user.DisplayName,
				Token = await _authService.CreateTokenAsync(user)
			});



		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			var checkUser = await _userManager.FindByEmailAsync(registerDto.Email);
			if(checkUser is null)
			{
				var newUser = new ApplicationUser()
				{
					DisplayName = registerDto.DisplayName,
					Email = registerDto.Email,
					PhoneNumber = registerDto.PhoneNumber,
					UserName = registerDto.Email.Split('@')[0]
				};
				var creatingNewUser = await _userManager.CreateAsync(newUser,registerDto.Password);

				if (!creatingNewUser.Succeeded)
					return BadRequest(new ApiValidationErrorResponse()
					{
						Errors = creatingNewUser.Errors.Select(E => E.Description).ToList()
					});
				return Ok(new UserDto()
				{
					DisplayName = registerDto.DisplayName,
					Email = registerDto.Email,
					Token = await _authService.CreateTokenAsync(newUser)
				}) ;

			}
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest,"Email already exists"));
		}

	}
}
