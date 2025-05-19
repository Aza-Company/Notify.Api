using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotifyHub.Domain.Aggregates;

namespace NotifyHub.Infrastructure.Persistence.Configurations;

public class SmsMessageConfiguration : IEntityTypeConfiguration<SmsMessage>
{
    public void Configure(EntityTypeBuilder<SmsMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasMany(x => x.SmsSendRequests)
               .WithOne()
               .HasForeignKey(x => x.SmsMessageId);
    }
}
