using DataSystem.Shared.DTOs.Response;
using DataSystem.Shared.DTOs.Task;

namespace DataSystem.Application.Task.Service
{

    public interface ITaskServices
    {
        Task<ServiceResponseDTO<TaskDTO>> Create(TaskCreateDTO taskCreateDTO);

        Task<ServiceResponseDTO<bool>> Delete(int id);

        Task<ServiceListResponseDTO<TaskDTO>> List(TaskListFilterDTO taskListFilterDTO);

        Task<ServiceResponseDTO<TaskDTO>> Read(int id);

        Task<ServiceResponseDTO<TaskDTO>> Update(TaskUpdateDTO taskUpdateDTO);
    }

}
