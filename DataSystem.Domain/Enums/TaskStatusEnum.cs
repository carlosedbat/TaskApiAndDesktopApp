using System.ComponentModel.DataAnnotations;

namespace DataSystem.Domain.Enums
{
    public enum TaskStatusEnum
    {
        [Display(Name = "Pendente")]
        Pending = 0,
        [Display(Name = "Em Progresso")]
        InProgress = 1,
        [Display(Name = "Concluído")]
        Completed = 2,

        [Display(Name = "Todos")]
        All = 3
    }
}
