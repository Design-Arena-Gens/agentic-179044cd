using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Configurations;

public class WorkLogConfiguration : IEntityTypeConfiguration<WorkLog>
{
    public void Configure(EntityTypeBuilder<WorkLog> builder)
    {
        builder.ToTable("WorkLogs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Notes).HasMaxLength(1000);

        builder.HasOne(x => x.UserProfile)
            .WithMany(x => x.WorkLogs)
            .HasForeignKey(x => x.UserProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
