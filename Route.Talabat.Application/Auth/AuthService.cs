using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Auth
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthService(UserManager<ApplicationUser> userManager , IConfiguration configuration)
		{
			_userManager = userManager;
			this._configuration = configuration;
		}
		public async Task<string> CreateTokenAsync(ApplicationUser user)
		{
			var privateClaims = new List<Claim>{
			 new Claim(ClaimTypes.Email, user.Email??"Empty email"),
			 new Claim(ClaimTypes.GivenName,user.DisplayName),
			 new Claim(ClaimTypes.MobilePhone,user.PhoneNumber ?? "Empty phoneNumber"),
			};

			var userRoles = await _userManager.GetRolesAsync(user);

			foreach (var role in userRoles)
				privateClaims.Add(new Claim(ClaimTypes.Role,role));

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]??"Empty Key"));

			var token = new JwtSecurityToken(
				audience : _configuration["JWT:Audience"],
				issuer: _configuration["JWT:Issuer"],
				expires: DateTime.Now.AddDays(int.Parse(_configuration["JWT:ExpirationDays"]??"30")),
				claims:privateClaims,
				signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
				);
			
			return new JwtSecurityTokenHandler().WriteToken(token);
			
			
		}
	}
}
