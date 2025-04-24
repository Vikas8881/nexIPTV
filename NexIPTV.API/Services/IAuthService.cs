using NexIPTV.API.DTOs;

namespace NexIPTV.API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginDto loginDto);
    }
}