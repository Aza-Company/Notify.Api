using MediatR;
using NotifyHub.Application.Extensions;

namespace NotifyHub.Application.Features.Commands.Auth;

public record MobileLoginCommand(string Token) : IRequest<Result<LoginResponse>>;
