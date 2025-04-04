using DataSystem.Shared.DTOs.Task;
using DataSystem.Domain.Enums;

namespace DataSystem.Test.Src.Task.Data
{
    public static class TaskTestData
    {
        public static IEnumerable<object[]> GetValidTaskCreateDtos()
        {
            yield return new object[]
            {
                new TaskCreateDTO
                {
                    Title = "Tarefa 1",
                    Description = "Descrição da tarefa 1",
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    CompletedAt = null,
                    Status = TaskStatusEnum.Pending
                }
            };

            yield return new object[]
            {
                new TaskCreateDTO
                {
                    Title = "Tarefa 2",
                    Description = "Descrição da tarefa 2",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    CompletedAt = DateTime.UtcNow,
                    Status = TaskStatusEnum.Completed
                }
            };
        }
    }
}
