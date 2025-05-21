using NotifyHub.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NotifyHub.Domain.Aggregates;

public class UserDevice : AuditableEntity
{
    [Key]
    public Guid UserId { get; private set; }
    public string DeviceName { get; private set; }
    public string DeviceToken { get; private set; }
    public bool IsOnline { get; private set; }
    public DateTime LastSeen { get; private set; }

    public User? User { get; private set; }

    private UserDevice() { }

    public UserDevice(Guid userId, string deviceName, string deviceToken)
    {
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
