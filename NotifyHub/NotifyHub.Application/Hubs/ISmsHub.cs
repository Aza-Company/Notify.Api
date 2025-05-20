using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Application.Hubs;

public interface ISmsHub
{
    Task DispatcherSmsAsync(UserDevice userDevice, SmsMessage smsMessage);
}
