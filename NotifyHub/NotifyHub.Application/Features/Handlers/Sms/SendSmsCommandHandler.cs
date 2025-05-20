using MediatR;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Application.Features.Commands.Sms;
using NotifyHub.Domain.Aggregates;
using NotifyHub.Domain.Interfaces;

namespace NotifyHub.Application.Features.Handlers.Sms;

internal sealed class SendSmsCommandHandler : IRequestHandler<SendSmsCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public SendSmsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        var message = new SmsMessage(request.UserId, request.Text, request.Recipients);

        var device = await _context.UserDevices
            .Where(d => d.UserId == request.UserId && d.IsOnline)
            .OrderByDescending(d => d.LastSeen)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new InvalidOperationException("Нет активного устройства у пользователя");

        await _context.SmsMessages.AddAsync(message, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
