
namespace DataSystem.Infraestructure.Src.Task.Repository
{
    using DataSystem.Domain.Task.Entity;
    using DataSystem.Domain.Task.Repository;
    using DataSystem.Infraestructure.Context;
    using DataSystem.Infraestructure.Src.Task.Repository.FilterBuilder;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<int> Count(TaskEntity taskFilter)
        {
            IQueryable<TaskEntity> query = this._appDbContext.Task;

            query = new TaskFilterBuilder(query)
                    .FilterByStatus(taskFilter.Status)
            .Build();

            return await query.CountAsync();
        }

        public async Task<List<TaskEntity>> List(TaskEntity taskFilter, int pageIndex, int itensByPage)
        {
            IQueryable<TaskEntity> query = this._appDbContext.Task;

            query = new TaskFilterBuilder(query)
                    .FilterByStatus(taskFilter.Status)
                        .Build();

            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            int skipedItens = (pageIndex - 1) * itensByPage;

            return await query.Skip(skipedItens).Take(itensByPage).ToListAsync();
        }

        public async Task<TaskEntity> Read(int id)
        {
            IQueryable<TaskEntity> query = this._appDbContext.Task;

            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskEntity> Update(TaskEntity currentTask, TaskEntity newTask)
        {
            this._appDbContext.Entry(currentTask).CurrentValues.SetValues(newTask);

            return newTask;
        }

        public async Task<TaskEntity> Delete(int id)
        {
            TaskEntity task = await this._appDbContext.Task.FirstOrDefaultAsync(t => t.Id == id);
            
            if(task != null)
            {
                this._appDbContext.Task.Remove(task);
                return task;
            }

            return task;
        }

        public async Task<TaskEntity> Create(TaskEntity entity)
        {
            try
            {
                await _appDbContext.Set<TaskEntity>().AddAsync(entity);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar tarefa no banco de dados.", ex);
            }
        }

    }

}
