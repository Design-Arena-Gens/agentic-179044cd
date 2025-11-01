using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Configurations;

public class StatementOfWorkConfiguration : IEntityTypeConfiguration<StatementOfWork>
{
    public void Configure(EntityTypeBuilder<StatementOfWork> builder)
    {
        builder.ToTable("StatementsOfWork");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Version).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ScopeOverview).HasMaxLength(2000);
        builder.Property(x => x.BillingFrequency).HasConversion<int>();

        builder.HasMany(x => x.Milestones)
            .WithOne(x => x.StatementOfWork)
            .HasForeignKey(x => x.StatementOfWorkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
