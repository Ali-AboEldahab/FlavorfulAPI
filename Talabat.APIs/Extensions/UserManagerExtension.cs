namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddress(this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            AppUser? user = await userManager.Users.Include(u => u.Address).Where(u => u.Email == email).SingleOrDefaultAsync();
            return user;
        }
    }
}
