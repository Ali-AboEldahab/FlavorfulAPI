using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (!_userManager.Users.Any())
            {
                AppUser user = new()
                {
                    DisplayName = "Ali Abo Eldahab",
                    Email = "aliaboeldahab@gmail.com",
                    UserName = "aliaboeldahab",
                    PhoneNumber = "01271122344"
                };

                await _userManager.CreateAsync(user, "Am@123456"/*password*/);
            }
        }
    }
}
