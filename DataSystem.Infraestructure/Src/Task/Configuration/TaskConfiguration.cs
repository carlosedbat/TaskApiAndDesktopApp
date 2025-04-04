namespace DataSystem.Infraestructure.Src.Task.Configuration
{
    using DataSystem.Domain.Task.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(property => property.Title)
              .HasColumnName("title")
              .HasMaxLength(100)
              .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(t => t.CompletedAt)
                .HasColumnName("completed_at")
                .IsRequired(false);

            builder.Property(t => t.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .IsRequired();
        }
    }

}
