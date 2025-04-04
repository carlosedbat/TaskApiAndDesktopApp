using DataSystem.Domain.Enums;
using DataSystem.FrontendWpf.Models.Api.DTO;
using DataSystem.Shared.DTOs.Task;
using Refit;

namespace DataSystem.FrontendWpf.Services.Api.Task
{
    public interface ITaskApi
    {
        // Obter todos os produtos com paginação
        [Get("/Task")]
        Task<ApiListResponseWrapperDTO<TaskDTO>> ListAsync([Query] TaskListFilterDTO taskListFilterDTO);

        [Get("/Task/{id}")]
        Task<ApiResponseDTO<TaskDTO>> ReadAsync(int id);

        [Post("/Task")]
        Task<ApiResponseDTO<TaskDTO>> CreateAsync([Body] TaskCreateDTO taskCreateDTO);

        [Put("/Task")]
        Task<ApiResponseDTO<TaskDTO>> UpdateAsync([Body] TaskUpdateDTO taskUpdateDTO);

        [Delete("/Task/{id}")]
        Task<ApiResponseDTO<bool>> DeleteAsync(int id);
    }
}
