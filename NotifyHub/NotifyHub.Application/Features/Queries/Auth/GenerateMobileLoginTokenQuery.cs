using MediatR;
using NotifyHub.Application.Extensions;

namespace NotifyHub.Application.Features.Queries.Auth;

public record GenerateMobileLoginTokenQuery() : IRequest<Result<string>>;
