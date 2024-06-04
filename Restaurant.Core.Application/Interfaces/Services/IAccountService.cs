using Restaurant.Core.Application.DTOs.Account;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAdminAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterWaiterAsync(RegisterRequest request);
    }
}
