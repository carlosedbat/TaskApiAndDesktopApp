using DataSystem.Domain.Enums;
using DataSystem.Shared.DTOs.Pagination;

namespace DataSystem.Shared.DTOs.Task
{
    public class TaskListFilterDTO : PaginationParamsDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public TaskStatusEnum? Status { get; set; } = TaskStatusEnum.All;
    }
}
