using NotifyHub.Domain.Common;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Domain.Aggregates;

public class SmsSendRequest : AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid SmsMessageId { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Status { get; private set; } = SmsStatus.Pending;
    public string? FailureReason { get; private set; }
    public DateTime? SentAt { get; private set; }

    private SmsSendRequest() { }

    public SmsSendRequest(Guid smsMessageId, string phoneNumber)
    {
        Id = Guid.NewGuid();
        SmsMessageId = smsMessageId;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsSent()
    {
        Status = SmsStatus.Sent;
        SentAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        Status = SmsStatus.Failed;
        FailureReason = reason;
    }
}
