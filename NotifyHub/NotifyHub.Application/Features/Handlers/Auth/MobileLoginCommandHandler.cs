using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using NotifyHub.Application.Extensions;
using NotifyHub.Application.Features.Commands.Auth;
using NotifyHub.Application.Interfaces;
using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Application.Features.Handlers.Auth;

internal sealed class MobileLoginCommandHandler : IRequestHandler<MobileLoginCommand, Result<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IDistributedCache _cache;
    private readonly IJwtTokenService _jwtTokenService;

    public MobileLoginCommandHandler(UserManager<User> userManager, IDistributedCache cache, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
    }

    public async Task<Result<LoginResponse>> Handle(MobileLoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var cacheKey = $"MobileLoginToken: {request.Token}";
        var userId = await _cache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrWhiteSpace(userId))
            throw new UnauthorizedAccessException("Invalid or expired token");

        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UnauthorizedAccessException("User not found");

        var jwtToken = _jwtTokenService.GenerateToken(user);

        var response = new LoginResponse(
            user.FirstName ?? "",
            user.LastName ?? "",
            jwtToken
            );

        return Result<LoginResponse>.Success(response);
    }
}
