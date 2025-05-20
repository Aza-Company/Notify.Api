using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Domain.Interfaces;

public interface IApplicationDbContext
{
    DbSet<SmsMessage> SmsMessages { get; set; }
    DbSet<SmsSendRequest> SmsSendRequests { get; set; }
    DbSet<UserDevice> UserDevices { get; set; }

    DatabaseFacade Database { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
