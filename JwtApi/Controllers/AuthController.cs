using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace JwtApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string username,string password)
        {
            if(username!="admin"|| password!="password")
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Role,"Admin")
            }; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyaafoginio2o3ino23ABCD"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims:claims, 
                expires:DateTime.Now.AddMinutes(30),
                signingCredentials:creds
                );
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = jwt });
        }
    }
}
