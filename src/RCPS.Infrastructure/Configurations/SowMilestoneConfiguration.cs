using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Configurations;

public class SowMilestoneConfiguration : IEntityTypeConfiguration<SowMilestone>
{
    public void Configure(EntityTypeBuilder<SowMilestone> builder)
    {
        builder.ToTable("SowMilestones");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.BillingType).HasConversion<int>();
    }
}
