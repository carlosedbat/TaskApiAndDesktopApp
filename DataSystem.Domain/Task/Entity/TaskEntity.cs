using DataSystem.Domain.Enums;

namespace DataSystem.Domain.Task.Entity
{
    public class TaskEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;
    }
}
