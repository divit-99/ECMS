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

        [HttpPost("signup")]
        [ServiceFilter(typeof(ValidateEmployeeEmailFilter))]
        public async Task<ActionResult> Signup(SignupDto dto)
        {
            var result = await _auth.SignupAsync(dto);
            if (!result)
                return BadRequest("Error while signing-up!");
            return Ok(new { message = "Signup successful!" });
        }

        [HttpPost("login")]
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
