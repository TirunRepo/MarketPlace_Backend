using MarketPlace.Common.APIResponse;
using MarketPlace.Common.DTOs;
using System.Security.Claims;

namespace MarketPlace.Business.Services.Interface
{
    public interface IAuthService
    {
        Task<APIResponse<LoginResponse>> LoginAsync(LoginRequest request);
        Task<APIResponse<AuthDto>> CheckAuth(ClaimsPrincipal user);
        Task<APIResponse<LoginResponse>> RefreshTokenAsync();
        Task<APIResponse<string>> LogoutAsync();
        Task<APIResponse<string>> RegisterAsync(RegisterUser dto);

    }
}
