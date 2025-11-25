using ECMS.API.DTOs.User;
using ECMS.API.Filters;
using ECMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="dto">Signup request details.</param>
        /// <returns>Success message if signup is successful.</returns>
        /// <response code="200">Signup successful.</response>
        /// <response code="400">Validation failed or signup error.</response>
        [HttpPost("signup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidateEmployeeEmailFilter))]
        public async Task<ActionResult> Signup(SignupDto dto)
        {
            var result = await _auth.SignupAsync(dto);
            if (!result)
                return BadRequest("Error while signing-up!");
            return Ok(new { message = "Signup successful!" });
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="dto">Login details (email & password).</param>
        /// <returns>User data and authentication result.</returns>
        /// <response code="200">Login successful.</response>
        /// <response code="400">Invalid credentials.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var result = await _auth.LoginAsync(dto);
            return Ok(new
            {
                message = "Login successful!",
                data = result
            });
        }
    }

}
