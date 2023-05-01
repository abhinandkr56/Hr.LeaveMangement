using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HR.LeaveManagement.Application.Contracts.Idnetity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity.Services;

public class AuthService :IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> _userManager,
        IOptions<JwtSettings> jwtSettings, SignInManager<ApplicationUser> signInManager)
    {
        this._userManager = _userManager;
        _jwtSettings = jwtSettings;
        _signInManager = signInManager;
    }
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new NotFoundException("login failed", "No user found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            throw new BadRequestException("invalid login req");
        }

        JwtSecurityToken token = await GenerateToken(user);

        return new AuthResponse()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email,
            UserName = user.UserName
        };
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }.Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));

        var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Value.Issuer,
            audience: _jwtSettings.Value.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.DurationInMinutes),
            signingCredentials: signInCredentials);

        return jwtSecurityToken;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.Lastname,
            UserName = request.UserName,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            return new RegistrationResponse() { UserId = user.Id };
        }
        else
        {
            throw new BadRequestException($"{result.Errors}");
        }
    }
}