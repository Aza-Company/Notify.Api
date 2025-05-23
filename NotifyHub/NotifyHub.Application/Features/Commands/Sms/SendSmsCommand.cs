using MediatR;

namespace NotifyHub.Application.Features.Commands.Sms;

public record SendSmsCommand(
    string Text, 
    List<string> Recipients) : IRequest<Guid>;
