namespace Talabat.Core.Services
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(AppUser user ,UserManager<AppUser> userManager);
    }
}
