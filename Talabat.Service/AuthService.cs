using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //Private Claims
            List<Claim>? authClaims = new()
            {
                new(ClaimTypes.GivenName, user.UserName),
                new(ClaimTypes.Email, user.Email) 
            };

            IList<string>? userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles) 
                authClaims.Add(new(ClaimTypes.Role, role));

            SymmetricSecurityKey? authKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            JwtSecurityToken? token = new(                
                audience: _configuration["JWT:Audience"],
                issuer: _configuration["JWT:Issuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"]!)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
