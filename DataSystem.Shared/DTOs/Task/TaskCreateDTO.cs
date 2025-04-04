using DataSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataSystem.Shared.DTOs.Task
{
    public class TaskCreateDTO : IValidatableObject
    {
        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O Título deve ter no máximo 100 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "A Descrição deve ter no máximo 1000 caracteres.")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CompletedAt.HasValue && CompletedAt.Value < CreatedAt)
            {
                yield return new ValidationResult(
                    "A data de conclusão não pode ser anterior à data de criação.",
                    new[] { nameof(CompletedAt) });
            }
        }
    }
}
