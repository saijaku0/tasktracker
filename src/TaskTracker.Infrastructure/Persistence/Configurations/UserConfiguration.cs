using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.UserDomain;

namespace TaskTracker.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Lastname)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(p => p.Email)
                .IsUnique();
            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.PasswordHash)
                .IsRequired();
            builder.Property(p => p.Role)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}