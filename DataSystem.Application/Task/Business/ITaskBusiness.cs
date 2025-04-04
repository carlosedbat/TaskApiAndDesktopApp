using DataSystem.Shared.DTOs.Response;
using DataSystem.Shared.DTOs.Task;

namespace DataSystem.Application.Task.Business
{

    public interface ITaskBusiness
    {
        Task<TaskDTO> Create(TaskCreateDTO taskDTO);

        Task<bool> Delete(int id);

        Task<ListResponseDTO<TaskDTO>> List(TaskListFilterDTO taskFilter, int pageIndex, int itensByPage);

        Task<TaskDTO> Read(int taskId);

        Task<TaskDTO> Update(TaskUpdateDTO taskDTO);
    }

}
