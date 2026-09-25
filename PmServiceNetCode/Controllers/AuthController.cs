using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pmService.Models;
using PmServiceNetCode.DTOs.PmServiceNetCode.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using PmServiceNetCode.Models;

namespace PmServiceNetCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MaznetModel _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(
            MaznetModel context,
            IConfiguration configuration,
            IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(
                    x => x.UserName_User == model.Username);

            if (user == null)
                return Unauthorized("Invalid username.");

            var passwordResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password_User!,
                model.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid password.");

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Code_User.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName_User ?? "")
            };

            // Add permission claims
            var permissions = user.UserRoles
                .SelectMany(x => x.Role.RolePermissions)
                .Select(x => x.Permission.Name)
                .Distinct();

            foreach (var permission in permissions)
            {
                claims.Add(
                    new Claim("permission", permission));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                message = "Login successful",
                userId = user.Code_User,
                username = user.UserName_User,
                token = jwt
            });
        }
    }
}