using MediatR;
using NotifyHub.Application.Extensions;

namespace NotifyHub.Application.Features.Commands.Auth;

public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
    ) : IRequest<Result>;
