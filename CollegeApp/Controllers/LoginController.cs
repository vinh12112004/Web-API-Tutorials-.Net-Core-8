using CollegeApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
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
        public ActionResult Login(LoginDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("pls provide password and username");
            }
            LoginResponseDTO reponse = new()
            {
                Username = model.Username
            };
            string audience = string.Empty;
            string issuer = string.Empty;
            if (model.Username == "vinh" && model.Password == "vinh")
            {
                byte[] key = null;
                if (model.Policy == "Local")
                {
                    audience = _configuration.GetValue<string>("LocalAudience");
                    issuer = _configuration.GetValue<string>("LocalIssuer");
                    key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForLocal"));

                }
                else if (model.Policy == "Google")
                {
                    audience = _configuration.GetValue<string>("GoogleAudience");
                    issuer = _configuration.GetValue<string>("GoogleIssuer");
                    key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForGoogle"));
                }
                else
                {
                    audience = _configuration.GetValue<string>("MicrosoftAudience");
                    issuer = _configuration.GetValue<string>("MicrosoftIssuer");
                    key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForMicrosoft"));
                }
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = issuer,
                    Audience = audience,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, "Admin")
                    }),
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                reponse.Token = tokenHandler.WriteToken(token);
            }
            else
            {
                return BadRequest("invalid username or password");
            }
            return Ok(reponse);
        }
    }
}
