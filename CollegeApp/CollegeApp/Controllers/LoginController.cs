using CollegeApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] 
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LoginResponseDto> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide username and password");
            }
            LoginResponseDto response = new LoginResponseDto() { Username = dto.Username };

            byte[] key = null;
            string issuer = string.Empty;
            string audience = string.Empty;

            if (dto.Policy == "Frontend")
            {
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:Frontend:Key"));
                issuer = _configuration["JWT:Frontend:Issuer"];
                audience = _configuration["JWT:Frontend:Audience"];
            }
            else if (dto.Policy == "Microsoft")
            {
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:MSUser:Key"));
                issuer = _configuration["JWT:MSUser:Issuer"];
                audience = _configuration["JWT:MSUser:Audience"];
            }               
            else if (dto.Policy == "Local")
            {
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:Local:Key"));
                issuer = _configuration["JWT:Local:Issuer"];
                audience = _configuration["JWT:Local:Audience"];
            }
                

            if (dto.Username == "Olaide" && dto.Password == "Olaide1996")
            {

                //if the username and password are correct we want to generate the token here for the client

                //var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForFrontend:Key"));
                var tokenHandler = new JwtSecurityTokenHandler(); //this is the object that create, read and validate the token
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    //The Subject defines the identity that wull be embedded in JWT
                    Subject = new ClaimsIdentity(new Claim[]                                        //ClaimIdentity represent the object identity in terms of claims
                     {
                         //username
                         new Claim(ClaimTypes.Name, dto.Username),
                         //roles
                         new Claim(ClaimTypes.Role, "Admin"),

                     }),

                    Expires = DateTime.Now.AddHours(4),

                    Issuer = issuer,
                    Audience = audience,

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenGenerated = tokenHandler.WriteToken(token); //this line convert the token to jwt string
                //response.Token = tokenHandler.WriteToken(token);
                response.Token = tokenGenerated;
            }
            
            else
            {
                return NotFound("Invalid username and password");
            } 
            return Ok(response);
        }
    }

}
