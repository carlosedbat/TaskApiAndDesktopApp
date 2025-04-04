using AutoMapper;
using DataSystem.Domain.Task.Entity;
using DataSystem.Shared.DTOs.Task;

namespace DataSystem.Shared.MappingProfiles.Task
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            this.CreateMap<TaskDTO, TaskUpdateDTO>().ReverseMap();
            this.CreateMap<TaskDTO, TaskCreateDTO>().ReverseMap();
            this.CreateMap<TaskDTO, TaskListFilterDTO>().ReverseMap();

            this.CreateMap<TaskDTO, TaskEntity>().ReverseMap();
            this.CreateMap<TaskEntity, TaskCreateDTO>().ReverseMap();
            this.CreateMap<TaskEntity, TaskUpdateDTO>().ReverseMap();
            this.CreateMap<TaskEntity, TaskListFilterDTO>().ReverseMap();
        }
    }

}
