using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymManagementSystem.Models;
using GymManagementSystem.Requests;
using GymManagementSystem.Responses;
using Microsoft.IdentityModel.Tokens;

namespace GymManagementSystem.Services
{
    public class JwtTokenProvider(IConfiguration configuration)
    {
        public TokenResponse GenerateToken(Person user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var key = jwtSettings["SecretKey"];
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier , user.Id.ToString()),
                new (ClaimTypes.Email , user.Email),
                new (ClaimTypes.Name , user.FullName),
                new (ClaimTypes.Role , user.Role)
            };
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!)
            );
            var credentials = new SigningCredentials(
                securityKey ,
                SecurityAlgorithms.HmacSha256
            );
            var token = new JwtSecurityToken(
                issuer : issuer,
                audience : audience,
                claims : claims,
                expires : expires,
                signingCredentials : credentials
            );

            return new TokenResponse
            {
                AccessToken =new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = Guid.NewGuid().ToString(),
                TimeExpires = expires

            };

        }
    }
}