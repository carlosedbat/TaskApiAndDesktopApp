namespace DataSystem.Infraestructure.Src.Task.Repository.FilterBuilder
{
    using DataSystem.Domain.Enums;
    using DataSystem.Domain.Task.Entity;
    using DataSystem.Shared.Utils;
    using System.Linq;

    public class TaskFilterBuilder
    {
        private IQueryable<TaskEntity> _query;

        public TaskFilterBuilder(IQueryable<TaskEntity> query)
        {
            _query = query;
        }

        public IQueryable<TaskEntity> Build()
        {
            return _query;
        }

        public TaskFilterBuilder FilterByStatus(TaskStatusEnum status)
        {
            if (status != TaskStatusEnum.All && EnumUtils.ValidateEnum<TaskStatusEnum>(status))
            {
                _query = _query.Where(u => u.Status == status);
            }
            return this;
        }

    }
}
