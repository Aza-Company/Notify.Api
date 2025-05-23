using MediatR;
using NotifyHub.Application.Features.Commands.Sms;
using NotifyHub.Application.Interfaces;
using NotifyHub.Domain.Aggregates;
using NotifyHub.Domain.Interfaces;

namespace NotifyHub.Application.Features.Handlers.Sms;

internal sealed class SendSmsCommandHandler : IRequestHandler<SendSmsCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public SendSmsCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public async Task<Guid> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var userId = _currentUserService.GetUserId();

        var message = new SmsMessage(userId, request.Text, request.Recipients);

        //var device = await _context.UserDevices
        //    .Where(d => d.UserId == userId && d.IsOnline)
        //    .OrderByDescending(d => d.LastSeen)
        //    .FirstOrDefaultAsync(cancellationToken)
        //    ?? throw new NoActiveDeviceException("Нет активного устройства у пользователя");

        await _context.SmsMessages.AddAsync(message, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
