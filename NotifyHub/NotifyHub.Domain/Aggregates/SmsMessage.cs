using NotifyHub.Domain.Common;

namespace NotifyHub.Domain.Aggregates;

public class SmsMessage : AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Text { get; private set; }

    private readonly List<SmsSendRequest> _smsSendRequests = new();
    public IReadOnlyCollection<SmsSendRequest> SmsSendRequests => _smsSendRequests;

    private SmsMessage() { }

    public SmsMessage(Guid userId, string text, IEnumerable<string> recipients)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Text = text;
        CreatedAt = DateTime.UtcNow;

        foreach (var recipient in recipients)
        {
            _smsSendRequests.Add(new SmsSendRequest(Id, recipient));
        }
    }

    public void MarkAsSent(string recipient)
    {
        var request = _smsSendRequests.FirstOrDefault(r => r.PhoneNumber == recipient);
        request?.MarkAsSent();
    }

    public void MarkAsFailed(string recipient, string reason)
    {
        var request = _smsSendRequests.FirstOrDefault(r => r.PhoneNumber == recipient);
        request?.MarkAsFailed(reason);
    }
}
