using MediatR;
using Microsoft.AspNetCore.Identity;
using NotifyHub.Application.Extensions;
using NotifyHub.Application.Features.Commands.Auth;
using NotifyHub.Application.Interfaces;
using NotifyHub.Domain.Aggregates;
using NotifyHub.Domain.Exceptions;

namespace NotifyHub.Application.Features.Handlers.Auth;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(UserManager<User> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new EntityNotFoundException("Пользователь с таким email не найден.");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
            throw new Domain.Exceptions.UnauthorizedAccessException("Неверный пароль.");

        var token = _jwtTokenService.GenerateToken(user);

        var response = new LoginResponse(
            user.FirstName,
            user.LastName,
            token
            );

        return Result<LoginResponse>.Success(response);
    }
}
