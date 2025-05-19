using NotifyHub.Domain.Common;

namespace NotifyHub.Domain.Aggregates;

public class UserDevice : AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string DeviceName { get; private set; }
    public string DeviceToken { get; private set; }
    public bool IsOnline { get; private set; }
    public DateTime LastSeen { get; private set; }

    private UserDevice() { }

    public UserDevice(Guid userId, string deviceName, string deviceToken)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        DeviceName = deviceName;
        DeviceToken = deviceToken;
        IsOnline = true;
        LastSeen = DateTime.UtcNow;
    }

    public void SetOffLine()
    {
        IsOnline = false;
        LastSeen = DateTime.UtcNow;
    }

    public void UpdateDeviceToken(string deviceToken)
    {
        DeviceToken = deviceToken;
        LastSeen = DateTime.UtcNow;
    }

    public void SetOnline()
    {
        IsOnline = true;
        LastSeen = DateTime.UtcNow;
    }
}
