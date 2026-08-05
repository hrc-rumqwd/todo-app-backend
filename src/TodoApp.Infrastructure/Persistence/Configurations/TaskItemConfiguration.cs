using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;
using TodoApp.Shared.Enums;

namespace TodoApp.Infrastructure.Persistence.Configurations
{
    internal class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("TaskItems");
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Priority)
                .IsRequired()
                .HasDefaultValue(TaskPriorities.Low.ToString());

            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(TaskStatuses.New.ToString());

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
