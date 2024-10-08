using Microsoft.AspNetCore.Mvc;
using jan24ft_bet_ca_kronosGR.Services;
using jan24ft_bet_ca_kronosGR.DTO;

namespace jan24ft_bet_ca_kronosGR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// Sample request:
        ///
        ///     {
        ///        "username": "kronos,
        ///        "password": "123456"
        ///     }
        /// 
        //POST /api/Auth/register
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO request)
        {
            if (await _authService.RegisterUserAsync(request.Username, request.Password))
            {
                return Ok("Registration Success");
            }

            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// User login
        /// </summary>
        /// Sample request:
        ///
        ///     {
        ///        "username": "kronos,
        ///        "password": "123456"
        ///     }
        /// 
        //GET /api/Auth/login
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO request)
        {
            if (await _authService.ValidateUserAsync(request.Username, request.Password))
            {
                var user = await _authService.GetUserByUsername(request.Username);
                var token = _authService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password");
        }
    }
}