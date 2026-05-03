using Serilog;

namespace CollegeApp.Model
{
    public class LoginResponseDto //this will return whether the login is successful or not
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
