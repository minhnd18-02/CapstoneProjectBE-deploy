using Application.IService;
using Application.ViewModels.UserDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenService _authenService;

        /// <summary>
        /// Constructor for the AuthenticationController.
        /// </summary>
        /// <param name="authent">The authentication service used to handle authentication requests.</param>
        public AuthenticationController(IAuthenService authent)
        {
            _authenService = authent;
        }

        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="registerObject">The registration details for the new user.</param>
        /// <returns>A response indicating success or failure of the registration.</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerObject)
        {
            var result = await _authenService.RegisterAsync(registerObject);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Logs in an existing user with the provided login details.
        /// </summary>
        /// <param name="loginObject">The login details including username and password.</param>
        /// <returns>A response indicating success or failure of the login, along with a token and user role if successful.</returns>
        [HttpPost("login")]
        [AllowAnonymous]

        public async Task<IActionResult> LoginAsync(LoginUserDTO loginObject)
        {
            var result = await _authenService.LoginAsync(loginObject);

            if (!result.Success)
            {
                return StatusCode(401, result);
            }
            else
            {
                return Ok(
                    new
                    {
                        success = result.Success,
                        message = result.Message,
                        token = result.DataToken,
                        role = result.Role,
                        hint = result.HintId,
                    }
                );
            }
        }
    }
}
