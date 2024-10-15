using Matcher.API.Models;
using Matcher.BLL.Interfaces;
using Matcher.DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Matcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBaseRepository<User> _userRep;
        private readonly IConfiguration _configuration;
        public AccountsController(IConfiguration configuration, IBaseRepository<User> userRep)
        {
            _configuration = configuration;
            _userRep = userRep;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AccountDTO user)
        {
            try
            {
                Response response = new();

                if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    response.IsSuccess = false;
                    response.Message = "Username or password format is not correct.";
                    return Ok(response);
                }

                User dbUser = await _userRep.GetAsync(filter: u => u.Username == user.Username && u.Password == user.Password && u.IsActive == true);

                if (dbUser == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid username or password.";
                    return Ok(response);
                }

                List<Claim> authClaims = new()
                {
                    new("username",dbUser.Username),
                    new("email",dbUser.Email),
                };

                JwtSecurityToken token = GetToken(authClaims);

                response.IsSuccess = true;
                response.Data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = dbUser
                };

                return Ok(response);
            }
            catch (Exception exc)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exc.Message);
            }

        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                Response response = new();

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User information is not correct.";
                    return Ok(response);
                }

                User dbUser = await _userRep.GetAsync(filter: u => u.Username == user.Username || u.Email == user.Email);

                if (dbUser != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Username or email already exists.";
                    return Ok(response);
                }

                await _userRep.Insert(user);
                response.IsSuccess = true;
                response.Message = "User has been created successfully.";
                return Ok(response);

            }
            catch (Exception exc)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exc.Message);
            }
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            JwtSecurityToken token = new(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
