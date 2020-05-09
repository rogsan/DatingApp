using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : ControllerBase
   {
      private readonly IConfiguration _configuration;
      private readonly IAuthRepository _authRepository;

      public AuthController(IConfiguration configuration, IAuthRepository authRepository)
      {
         this._configuration = configuration;
         this._authRepository = authRepository;
      }

      [HttpPost("register")]
      public async Task<IActionResult> Register(UserForRegisterDTO user)
      {
        user.Username = user.Username.ToLower();

        if (await _authRepository.UserExists(user.Username))
        {
            return BadRequest("Username already exists");
        }

        var userToCreate = new User
        {
            Username = user.Username,
        };

        var createdUser = await _authRepository.Register(userToCreate, user.Password);

        return StatusCode(201);
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login(UserForLoginDTO user)
      {
         var userFromRepo = await _authRepository.Login(user.Username.ToLower(), user.Password);

         if (userFromRepo == null)
         {
            return Unauthorized();
         }

         var claims = new[]
         {
            new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromRepo.Username)
         };

         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

         var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
         };

         var tokenHandler = new JwtSecurityTokenHandler();

         var token = tokenHandler.CreateToken(tokenDescriptor);

         return Ok(new {
            token = tokenHandler.WriteToken(token)
         });
      }
   }
}