using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Configurations;

public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.ToTable("Reminders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReminderType).HasConversion<int>();
        builder.Property(x => x.Message).HasMaxLength(500);
    }
}
