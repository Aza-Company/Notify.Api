using MediatR;

namespace NotifyHub.Application.Features.Commands.Sms;

public record SendSmsCommand(
    Guid UserId, 
    string Text, 
    List<string> Recipients) : IRequest<Guid>;
