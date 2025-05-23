using MediatR;
using Microsoft.AspNetCore.Identity;
using NotifyHub.Application.Extensions;
using NotifyHub.Application.Features.Commands.Auth;
using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Application.Features.Handlers.Auth;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly UserManager<User> _userManager;

    public RegisterCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new Exception(string.Join(", ", errors));
        }

        return Result.Success();
    }
}
