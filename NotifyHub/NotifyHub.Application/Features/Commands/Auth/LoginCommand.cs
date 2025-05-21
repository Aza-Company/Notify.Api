using MediatR;
using NotifyHub.Application.Extensions;

namespace NotifyHub.Application.Features.Commands.Auth;

public record LoginCommand(
    string Email, 
    string Password
    ) : IRequest<Result<LoginResponse>>;

public record LoginResponse(
    string FirstName,
    string LastName,
    string Token
);
