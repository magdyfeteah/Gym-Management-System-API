using GymManagementSystem.Data;
using GymManagementSystem.Requests;
using GymManagementSystem.Responses;
using GymManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(AppDbContext context , JwtTokenProvider jwtToken ,AuthService auth) : ControllerBase
    {
        private AppDbContext _context = context;

        private JwtTokenProvider _jwtToken = jwtToken;

        [HttpPost("login")]
        [EndpointSummary("Login user")]
        [EndpointDescription("Authenticates a user and returns a JWT token.")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequest request )
        {
            var token = await auth.LoginAsync(request) ;

            if (token is null)
                return Unauthorized("Invalid email or password!");
            return Ok(token);
        }
    }
}