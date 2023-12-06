using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
//using Nutricion.API.Migrations;
using Nutricion.API.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nutricion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo model)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else 
            { 
                return BadRequest("Login Fallido");
            }
        }

        [HttpPost("registro")]
        public async Task<ActionResult<UserToken>> RegistroUsuario([FromBody] UserInfo userInfo)
        {
            var user = new IdentityUser
            { UserName = userInfo.UserName, Email = userInfo.Email };

            var result = await userManager.CreateAsync(user, userInfo.Password);

            if (result.Succeeded)
            {
                return BuildToken(userInfo);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            var calims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name,userInfo.UserName),
        new Claim(ClaimTypes.Email,userInfo.Email),
        new Claim("Password",userInfo.Password),

    };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["jwtkey"]!));

            var credential = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: calims,
                expires: expiration,
                signingCredentials: credential);

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler
                ().WriteToken(token),
                Expiracion = expiration
            };
        }
    }
}
