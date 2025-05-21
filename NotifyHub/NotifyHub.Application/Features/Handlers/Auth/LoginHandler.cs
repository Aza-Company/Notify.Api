using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotifyHub.Application.Configurations;
using NotifyHub.Application.Extensions;
using NotifyHub.Application.Features.Commands.Auth;
using NotifyHub.Domain.Aggregates;
using NotifyHub.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotifyHub.Application.Features.Handlers.Auth;

internal sealed class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _options;

    public LoginHandler(UserManager<User> userManager, IOptions<JwtOptions> options)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new EntityNotFoundException("Пользователь с таким email не найден.");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
            throw new Domain.Exceptions.UnauthorizedAccessException("Неверный пароль.");

        var token = GenerateToken(user);

        var response = new LoginResponse(
            user.FirstName,
            user.LastName,
            token
            );

        return Result<LoginResponse>.Success(response);
    }

    public string GenerateToken(User user)
    {
        var claims = GetClaims(user);
        var signingKey = GetClaimingKey();
        var securityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            signingCredentials: signingKey,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresInHours));

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

    private static List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!)
        };

        return claims;
    }

    private SigningCredentials GetClaimingKey()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return signingKey;
    }
}
