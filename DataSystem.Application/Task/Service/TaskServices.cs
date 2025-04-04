namespace DataSystem.Application.Task.Service
{
    using AutoMapper;
    using DataSystem.Application.Task.Business;
    using DataSystem.Domain.UnitOfWork;
    using DataSystem.Shared.Constants.Pagination;
    using DataSystem.Shared.DTOs.Pagination;
    using DataSystem.Shared.DTOs.Response;
    using DataSystem.Shared.DTOs.Task;
    using DataSystem.Shared.Exceptions;
    using DataSystem.Shared.Utils;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    public class TaskServices : ITaskServices
    {
        public readonly ITaskBusiness _taskBusiness;
        private readonly IWorkUnit _iWorkUnit;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskServices> _iLogger;

        public TaskServices(
            ITaskBusiness taskBusiness,
            IWorkUnit workUnit,
            IMapper mapper,
            ILogger<TaskServices> logger)
        {
            this._taskBusiness = taskBusiness;
            this._mapper = mapper;
            this._iWorkUnit = workUnit;
            this._iLogger = logger;

        }

        public async Task<ServiceResponseDTO<TaskDTO>> Create(TaskCreateDTO taskCreateDTO)
        {
            ServiceResponseDTO<TaskDTO> serviceResponseDTO = new ServiceResponseDTO<TaskDTO>();
            try
            {
                TaskDTO task = await this._taskBusiness.Create(taskCreateDTO);

                serviceResponseDTO.GenericData = this._mapper.Map<TaskDTO>(task);
                serviceResponseDTO.StatusCode = StatusCodes.Status201Created;

                await this._iWorkUnit.SaveChangesAsync();
                await this._iWorkUnit.CommitAsync();
            }
            catch (CustomException ex)
            {
                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, TaskDTO>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, TaskDTO>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<bool>> Delete(int id)
        {
            ServiceResponseDTO<bool> serviceResponseDTO = new ServiceResponseDTO<bool>();
            try
            {
                bool result = await this._taskBusiness.Delete(id);

                serviceResponseDTO.GenericData = result;
                serviceResponseDTO.StatusCode = StatusCodes.Status200OK;

                await this._iWorkUnit.SaveChangesAsync();
                await this._iWorkUnit.CommitAsync();
            }
            catch (CustomException ex)
            {
                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, bool>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, bool>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }

        public async Task<ServiceListResponseDTO<TaskDTO>> List(TaskListFilterDTO taskListFilter)
        {
            ServiceResponseDTO<List<TaskDTO>> serviceResponseDTO = new ServiceResponseDTO<List<TaskDTO>>();
            ServiceListResponseDTO<TaskDTO> response = new ServiceListResponseDTO<TaskDTO>();
            try
            {
                ListResponseDTO<TaskDTO> taskDTOs = await this._taskBusiness.List(taskListFilter, taskListFilter.PageIndex, taskListFilter.PageSize);

                serviceResponseDTO.GenericData = this._mapper.Map<List<TaskDTO>>(taskDTOs.Data);
                serviceResponseDTO.TotalPages = taskDTOs.TotalPages;
                serviceResponseDTO.StatusCode = StatusCodes.Status200OK;

                await this._iWorkUnit.CommitAsync();

                response.ServiceResponseDTO = serviceResponseDTO;

                response.PaginationDTO = new PaginationDTO(
                    taskListFilter.PageSize,
                    taskDTOs.TotalCount.Value,
                    taskListFilter.PageIndex);
            }
            catch (CustomException ex)
            {
                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, List<TaskDTO>>(ex, ex.StatusCode);

                response.PaginationDTO = new PaginationDTO(
                    taskListFilter.PageSize,
                    DefaultPaginationValuesConst.DEFAULT_TOTAL_COUNT,
                    DefaultPaginationValuesConst.DEFAULT_PAGE_INDEX);

                response.ServiceResponseDTO = serviceResponseDTO;
            }
            catch (Exception ex)
            {
                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, List<TaskDTO>>(ex, HttpStatusCode.InternalServerError);

                response.PaginationDTO = new PaginationDTO(
                    taskListFilter.PageSize,
                    DefaultPaginationValuesConst.DEFAULT_TOTAL_COUNT,
                    DefaultPaginationValuesConst.DEFAULT_PAGE_INDEX);

                response.ServiceResponseDTO = serviceResponseDTO;
            }

            return response;
        }

        public async Task<ServiceResponseDTO<TaskDTO>> Read(int id)
        {
            ServiceResponseDTO<TaskDTO> serviceResponseDTO = new ServiceResponseDTO<TaskDTO>();
            try
            {
                TaskDTO taskDTO = await this._taskBusiness.Read(id);

                serviceResponseDTO.GenericData = this._mapper.Map<TaskDTO>(taskDTO);
                serviceResponseDTO.StatusCode = StatusCodes.Status200OK;

                await this._iWorkUnit.CommitAsync();
            }
            catch (CustomException ex)
            {
                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, TaskDTO>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, TaskDTO>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<TaskDTO>> Update(TaskUpdateDTO taskUpdateDTO)
        {
            ServiceResponseDTO<TaskDTO> serviceResponseDTO = new ServiceResponseDTO<TaskDTO>();
            try
            {
                TaskDTO taskDTO = await this._taskBusiness.Update(taskUpdateDTO);

                serviceResponseDTO.GenericData = this._mapper.Map<TaskDTO>(taskDTO);
                serviceResponseDTO.StatusCode = StatusCodes.Status200OK;

                await this._iWorkUnit.SaveChangesAsync();
                await this._iWorkUnit.CommitAsync();
            }
            catch (CustomException ex)
            {
                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, TaskDTO>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                this._iWorkUnit.Rollback();

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, TaskDTO>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }
    }

}
