using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Configurations;

public class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.ToTable("ProjectRoles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RoleName).IsRequired().HasMaxLength(150);

        builder.HasOne(x => x.UserProfile)
            .WithMany(x => x.ProjectRoles)
            .HasForeignKey(x => x.UserProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
