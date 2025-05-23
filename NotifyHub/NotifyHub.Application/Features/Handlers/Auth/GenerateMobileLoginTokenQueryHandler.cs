using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using NotifyHub.Application.Extensions;
using NotifyHub.Application.Features.Queries.Auth;
using NotifyHub.Application.Interfaces;

namespace NotifyHub.Application.Features.Handlers.Auth;

internal sealed class GenerateMobileLoginTokenQueryHandler : IRequestHandler<GenerateMobileLoginTokenQuery, Result<string>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDistributedCache _cache;

    public GenerateMobileLoginTokenQueryHandler(ICurrentUserService currentUserService, IDistributedCache cache)
    {
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<Result<string>> Handle(GenerateMobileLoginTokenQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var token = Guid.NewGuid().ToString();
        var cacheKey = $"MobileLoginToken: {token}";

        await _cache.SetStringAsync(cacheKey, userId.ToString(),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(6)
            }, cancellationToken);

        return Result<string>.Success(token);
    }
}
