namespace DataSystem.Domain.Task.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataSystem.Domain.Task.Entity;

    public interface ITaskRepository
    {
        Task<List<TaskEntity>> List(TaskEntity taskFilter, int pageIndex, int itensByPage);

        Task<TaskEntity> Read(int id);

        Task<TaskEntity> Update(TaskEntity currentTask, TaskEntity newTask);

        Task<int> Count(TaskEntity taskFilter);

        Task<TaskEntity> Delete(int id);

        Task<TaskEntity> Create(TaskEntity task);
    }
}
