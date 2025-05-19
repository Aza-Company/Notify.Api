using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Infrastructure.Persistence.Configurations;

public class SmsSendRequestConfiguration : IEntityTypeConfiguration<SmsSendRequest>
{
    public void Configure(EntityTypeBuilder<SmsSendRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasMaxLength(50);
    }
}
