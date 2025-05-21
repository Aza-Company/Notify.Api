using Microsoft.AspNetCore.Identity;

namespace NotifyHub.Domain.Aggregates;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public UserDevice? UserDevice { get; set; }
}
