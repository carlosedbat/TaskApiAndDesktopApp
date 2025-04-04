using DataSystem.Domain.Enums;
using DataSystem.Shared.Utils;
using System.ComponentModel.DataAnnotations;

namespace DataSystem.Shared.DTOs.Task
{
    public class TaskUpdateDTO : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O Título deve ter no máximo 100 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "A Descrição deve ter no máximo 1000 caracteres.")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public TaskStatusEnum Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CompletedAt.HasValue && CompletedAt.Value < CreatedAt)
            {
                yield return new ValidationResult(
                    "A data de conclusão não pode ser anterior à data de criação.",
                    new[] { nameof(CompletedAt) });
            }
        }

        public void UpdateFromDTO(TaskUpdateDTO newTaskDTO)
        {
            var builder = new DtoValidationBuilder<TaskUpdateDTO>(this);

            builder
                .UpdateProperty(
                    dto => dto.Title,
                    newTaskDTO.Title)
                .UpdateProperty(
                    dto => dto.Description,
                    newTaskDTO.Description)
                .UpdateProperty(
                    dto => dto.CreatedAt,
                    newTaskDTO.CreatedAt)
                .UpdateProperty(
                    dto => dto.CompletedAt,
                    newTaskDTO.CompletedAt)
                .UpdateEnum(
                    dto => dto.Status,
                    newTaskDTO.Status,
                    EnumUtils.ValidateEnum);
        }
    }
}
