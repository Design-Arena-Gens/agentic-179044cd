using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Department).HasMaxLength(50);
    }
}
