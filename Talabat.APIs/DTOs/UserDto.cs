namespace Talabat.APIs.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public UserDto(string displayName, string email , string token)
        {
            DisplayName = displayName;
            Email = email;
            Token = token;
        }
    }
}
