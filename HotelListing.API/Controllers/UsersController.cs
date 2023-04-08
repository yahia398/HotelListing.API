using HotelListing.API.Contracts;
using HotelListing.API.Models.ModelsDto.ApiUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IAuthManager authManager, ILogger<UsersController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] ApiUserDto apiUserDto)
        {
            _logger.LogInformation($"Registeration attempt for {apiUserDto.Email}");

            try
            {
                var errors = await _authManager.RegisterAsync(apiUserDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)} - Registeration attempt for {apiUserDto.Email}");
                return Problem($"Something went wrong in the {nameof(Register)} - please contact the support", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("admin/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAdmin([FromBody] ApiUserDto apiUserDto)
        {
            _logger.LogInformation($"Admin Registeration attempt for {apiUserDto.Email}");

            try
            {
                var errors = await _authManager.RegisterAdminAsync(apiUserDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterAdmin)} - Admin Registeration attempt for {apiUserDto.Email}");
                return Problem($"Something went wrong in the {nameof(RegisterAdmin)} - please contact the support", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation($"Login attempt for {loginDto.Email}");

            try
            {
                var authResponse = await _authManager.LoginAsync(loginDto);

                if (authResponse == null)
                {
                    _logger.LogWarning($"The login attempt was unsuccessful due to an incorrect username or password. Specifically, the {loginDto.Email} was unable to authenticate.");
                    return Unauthorized();
                }
                return Ok(authResponse);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)} - Login attempt for {loginDto.Email}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
                return Unauthorized();

            return Ok(authResponse);
        }
    }
}
