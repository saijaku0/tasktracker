using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain;

namespace TaskTracker.Infrastructure.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.Description)
                .HasMaxLength(1000);
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(p => p.AssignedUserId)
                .IsRequired(false);
        }
    }
}