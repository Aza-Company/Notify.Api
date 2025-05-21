using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Infrastructure.Persistence.Configurations;

public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
{
    public void Configure(EntityTypeBuilder<UserDevice> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.DeviceName)
            .HasMaxLength(100);

        builder.Property(x => x.DeviceToken)
            .HasMaxLength(200);

        builder.HasOne(d => d.User)
            .WithOne(u => u.UserDevice)
            .HasForeignKey<UserDevice>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
