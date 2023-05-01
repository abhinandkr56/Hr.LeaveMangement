
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Contracts.Idnetity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}