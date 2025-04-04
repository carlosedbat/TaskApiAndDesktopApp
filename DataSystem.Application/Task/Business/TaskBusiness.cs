namespace DataSystem.Application.Task.Business
{
    using AutoMapper;
    using DataSystem.Domain.Enums;
    using DataSystem.Domain.Task.Entity;
    using DataSystem.Domain.Task.Repository;
    using DataSystem.Shared.Constants.Messages;
    using DataSystem.Shared.DTOs.Response;
    using DataSystem.Shared.DTOs.Task;
    using DataSystem.Shared.Exceptions;
    using DataSystem.Shared.Helpers.Log;
    using DataSystem.Shared.Utils;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    public class TaskBusiness : ITaskBusiness
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _iTaskRepository;
        private readonly ILogger<TaskBusiness> _logger;

        public TaskBusiness(
            IMapper mapper,
            ITaskRepository iTaskRepository,
            ILogger<TaskBusiness> logger)
        {
            this._mapper = mapper;
            this._iTaskRepository = iTaskRepository;
            this._logger = logger;
        }

        public async Task<TaskDTO> Create(TaskCreateDTO taskDTO)
        {
            try
            {
                TaskEntity task = this._mapper.Map<TaskEntity>(taskDTO);

                this.ValidateTask(task);

                TaskEntity taskCreated = await this._iTaskRepository.Create(task);

                TaskDTO taskCreatedDTO = taskCreated is not null ? this._mapper.Map<TaskDTO>(taskCreated)
                    : throw new CustomException(HttpStatusCode.InternalServerError, BadRequestMessages.TaskNotCreated, new HttpRequestException());

                return taskCreatedDTO;
            }
            catch(CustomException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                LoggerHelper.LogError(_logger, "Erro ao criar tarefa.",ex);
                throw new CustomException(HttpStatusCode.InternalServerError, BadRequestMessages.TaskNotCreated, ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                TaskEntity task = await this._iTaskRepository.Read(id);

                if (task is null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, NotFoundMessages.TaskNotFound, new HttpRequestException());
                }

                task = await this._iTaskRepository.Delete(task.Id);

                return true;
            }
            catch (CustomException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(_logger, "Erro ao deletar tarefa.", ex);
                throw new CustomException(HttpStatusCode.InternalServerError, NotFoundMessages.TaskNotFound, ex);
            }
        }

        public async Task<ListResponseDTO<TaskDTO>> List(TaskListFilterDTO taskFilter, int pageIndex, int itensByPage)
        {
            try
            {
                TaskEntity taskEntityFilter = this._mapper.Map<TaskEntity>(taskFilter);

                List<TaskEntity> task = await this._iTaskRepository.List(taskEntityFilter, pageIndex, itensByPage);

                List<TaskDTO> taskDTOs = task.Count != 0 ? this._mapper.Map<List<TaskDTO>>(task)
                    : new List<TaskDTO>();

                int quantityOfTask = await this._iTaskRepository.Count(taskEntityFilter);

                int totalPages = MathAdvanced.DivideAndRoundUp(quantityOfTask, itensByPage);

                ListResponseDTO<TaskDTO> responseTask = new ListResponseDTO<TaskDTO>()
                {
                    Data = taskDTOs,
                    TotalPages = totalPages,
                    TotalCount = quantityOfTask,
                };

                return responseTask;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(_logger, "Erro ao listar tarefas.", ex);
                throw new CustomException(HttpStatusCode.InternalServerError, BadRequestMessages.TaskNotCreated, ex);
            }
        }

        public async Task<TaskDTO> Read(int taskId)
        {
            try
            {
                TaskEntity task = await this._iTaskRepository.Read(taskId);

                TaskDTO taskDTO = task is not null ? this._mapper.Map<TaskDTO>(task)
                      : throw new CustomException(HttpStatusCode.NotFound, NotFoundMessages.TaskNotFound, new HttpRequestException());

                return taskDTO;
            }
            catch (CustomException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(_logger, "Erro ao ler tarefa.", ex);
                throw new CustomException(HttpStatusCode.InternalServerError, BadRequestMessages.TaskNotCreated, ex);
            }
        }

        public async Task<TaskDTO> Update(TaskUpdateDTO taskDTO)
        {
            try
            {
                TaskEntity taskOld = await this._iTaskRepository.Read(taskDTO.Id);

                TaskUpdateDTO newTaskDTO = taskOld is not null ? this._mapper.Map<TaskUpdateDTO>(taskOld)
                        : throw new CustomException(HttpStatusCode.NotFound, NotFoundMessages.TaskNotFound, new HttpRequestException());

                newTaskDTO.UpdateFromDTO(taskDTO);

                TaskEntity newTask = newTaskDTO is not null ? this._mapper.Map<TaskEntity>(newTaskDTO)
                        : throw new CustomException(HttpStatusCode.InternalServerError, NotFoundMessages.TaskNotFound, new AutoMapperMappingException());

                newTask = await this._iTaskRepository.Update(taskOld, newTask);

                TaskDTO taskUpdated = newTask is not null ? this._mapper.Map<TaskDTO>(newTask)
                        : throw new CustomException(HttpStatusCode.InternalServerError, NotFoundMessages.TaskNull, new AutoMapperMappingException());

                return taskUpdated;
            }
            catch (CustomException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(_logger, "Erro ao atualizar tarefa.", ex);
                throw new CustomException(HttpStatusCode.InternalServerError, BadRequestMessages.TaskNotCreated, ex);
            }
        }

        //TODO [FEAT] implementar strings em constantes para mensagens de erro
        private bool ValidateTask(TaskEntity task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Titulo inválido", new HttpRequestException());
            }

            if(task.CompletedAt != null && task.CompletedAt < task.CreatedAt)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Data de conclusão não pode ser anterior a data de criação", new HttpRequestException());
            }

            if(task.Status == TaskStatusEnum.Completed && task.CompletedAt == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Data de conclusão não pode ser nula", new HttpRequestException());
            }

            if(task.Title.Length > 100)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Titulo não pode ter mais de 100 caracteres", new HttpRequestException());
            }

            if(task.Description?.Length > 1000)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Descrição não pode ter mais de 1000 caracteres", new HttpRequestException());
            }

            return true;
        }
    }

}
