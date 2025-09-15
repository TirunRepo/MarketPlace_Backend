    using MarketPlace.Business.Interfaces;
    using MarketPlace.Common.DTOs;
    using MarketPlace.DataAccess.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var result = await _auth.LoginAsync(req);
            if (!result.Success) return Unauthorized(result);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            var result = await _auth.CheckAuth(User);
            if (!result.Success) return Unauthorized(result);
            return Ok(result); // APIResponse<AuthDto>
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await _auth.RefreshTokenAsync();
            if (!result.Success) return Unauthorized(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _auth.LogoutAsync();
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auth.RegisterAsync(dto);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
