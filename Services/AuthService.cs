using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Requests;
using GymManagementSystem.Responses;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Services
{
    public class AuthService(AppDbContext context , JwtTokenProvider jwtToken)
    {
        public AppDbContext _context  = context;

        public JwtTokenProvider _jwtToken  = jwtToken;

        public async Task<TokenResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Set<Person>().FirstOrDefaultAsync(x=>x.Email == request.Email);

            if (user is null)
            {
                return null;
            }
            var validPassword = BCrypt.Net.BCrypt.Verify(request.Password , user.Password);
            if (!validPassword)
            {
                return null;
            }

            return _jwtToken.GenerateToken(user);
        }
    }
}