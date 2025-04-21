using Azure;
using GDB.Web.Common.Helpers;
using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IConfiguration configuration;
        private IUserRepository userRepository { get; set; }
        public UserController(IUserRepository _userRepository,
                                 ILogger<UserController> _logger,
                                 IConfiguration _configuration)
        {
            userRepository = _userRepository;
            logger = _logger;
            configuration = _configuration;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                var user = await userRepository.AuthenticateUser(model.EmailId, model.Password);
                
                if (user == null) return Unauthorized();

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var Token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(Token).ToString();
               
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing your request.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = ex.Message,
                    Details = ex.StackTrace
                });
            }

        }

    }
}
