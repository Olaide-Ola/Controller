using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Model
{
    public class LoginDto //this is what the client will sign in with
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string Policy { get; set; }
    }
}
//Write a confirm password here