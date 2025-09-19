using AutoMapper;
using MarketPlace.Business.Services.Interface;
using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs;
using MarketPlace.DataAccess.DBContext;
using MarketPlace.DataAccess.Entities;
using MarketPlace.Infrastucture.JwtTokenGenerator;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MarketPlace.Business.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly JwtTokenGenerator _jwt;
        private readonly IHttpContextAccessor _http;
        private readonly IUserRepository _users;

        public AuthService(AppDbContext ctx, IMapper mapper, JwtTokenGenerator jwt, IHttpContextAccessor http, IUserRepository users)
        {
            _ctx = ctx;
            _mapper = mapper;
            _jwt = jwt;
            _http = http;
            _users = users;
        }

        private CookieOptions BuildCookieOptions(TimeSpan lifetime, bool crossSite = true)
        {
            var isHttps = _http.HttpContext!.Request.IsHttps;
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = isHttps || _http.HttpContext!.Request.Host.Host.Contains("localhost") == false, // 🔑 allow non-secure only on localhost
                SameSite = crossSite ? SameSiteMode.None : SameSiteMode.Lax,
                Expires = DateTime.UtcNow.Add(lifetime),
                IsEssential = true,
                Path = "/"
            };
        }


        public async Task<APIResponse<LoginResponse>> LoginAsync(LoginRequest req)
        {
            var user = await _users.GetByEmailAsync(req.UserName);
            if(user == null)
            {
                user = await _users.GetByPhoneNumberAsync(req.UserName);
            }
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            {
                return new APIResponse<LoginResponse> { Success = false, Message = "Invalid credentials" };
            }

            // Generate tokens
            var accessToken = _jwt.GenerateToken(user.Id, user.Role, user.Email, TimeSpan.FromMinutes(120));
            var refreshToken = _jwt.GenerateRefreshToken();

            // persist refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _users.SaveChangesAsync();

            // set cookies
            _http.HttpContext!.Response.Cookies.Append("accessToken", accessToken, BuildCookieOptions(TimeSpan.FromMinutes(30)));
            _http.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, BuildCookieOptions(TimeSpan.FromDays(7)));

            var dto = _mapper.Map<LoginResponse>(user);

            return new APIResponse<LoginResponse> { Success = true, Data = dto, Message = "Login successful" };
        }

        public async Task<APIResponse<AuthDto>> CheckAuth(ClaimsPrincipal user)
        {
            var idStr = user.FindFirst("id")?.Value;
            if (!int.TryParse(idStr, out var userId))
                return new APIResponse<AuthDto> { Success = false, Message = "User id missing in token" };

            var dbUser = await _users.GetByIdAsync(userId);
            if (dbUser == null)
                return new APIResponse<AuthDto> { Success = false, Message = "User not found" };

            var dto = _mapper.Map<AuthDto>(dbUser);
            return new APIResponse<AuthDto> { Success = true, Data = dto, Message = "Token valid" };
        }

        public async Task<APIResponse<LoginResponse>> RefreshTokenAsync()
        {
            var ctx = _http.HttpContext!;
            if (!ctx.Request.Cookies.TryGetValue("refreshToken", out var refresh) || string.IsNullOrWhiteSpace(refresh))
                return new APIResponse<LoginResponse> { Success = false, Message = "Refresh token missing" };

            var user = await _users.GetByRefreshTokenAsync(refresh);
            if (user == null || user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                return new APIResponse<LoginResponse> { Success = false, Message = "Invalid or expired refresh token" };

            // issue new tokens
            var newAccess = _jwt.GenerateToken(user.Id, user.Role, user.Email, TimeSpan.FromMinutes(30));
            var newRefresh = _jwt.GenerateRefreshToken();

            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _users.SaveChangesAsync();

            ctx.Response.Cookies.Append("accessToken", newAccess, BuildCookieOptions(TimeSpan.FromMinutes(30)));
            ctx.Response.Cookies.Append("refreshToken", newRefresh, BuildCookieOptions(TimeSpan.FromDays(7)));

            var dto = _mapper.Map<LoginResponse>(user);
            return new APIResponse<LoginResponse> { Success = true, Data = dto, Message = "Tokens refreshed" };
        }

        public async Task<APIResponse<string>> LogoutAsync()
        {
            var ctx = _http.HttpContext!;
            ctx.Response.Cookies.Delete("accessToken");
            ctx.Response.Cookies.Delete("refreshToken");

            var idStr = ctx.User.FindFirst("id")?.Value;
            if (int.TryParse(idStr, out var userId))
            {
                var user = await _users.GetByIdAsync(userId);
                if (user != null)
                {
                    user.RefreshToken = null;
                    user.RefreshTokenExpiry = null;
                    await _users.SaveChangesAsync();
                }
            }
            return new APIResponse<string> { Success = true, Message = "Logged out" };
        }
        public async Task<APIResponse<string>> RegisterAsync(RegisterUser dto)
        {
            // Check if email already exists
            var existingUserByMail = await _users.GetByEmailAsync(dto.Email);
            if (existingUserByMail != null)
            {
                return new APIResponse<string> { Success = false, Message = "Email already exists" };
            }
            else
            {
                var existingUserByPhone = await _users.GetByPhoneNumberAsync(dto.PhoneNumber);
                if (existingUserByPhone != null) return new APIResponse<string> { Success = false, Message = "User already exists by same phoneNumber" };
            }

                // Hash the password here explicitly
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Map DTO to User entity
            var user = _mapper.Map<User>(dto);
            user.PasswordHash = hashedPassword;

            // Add user and save
            _users.Add(user);
            await _users.SaveChangesAsync();

            return new APIResponse<string> { Success = true, Message = "User registered successfully" };
        }
    }
}
