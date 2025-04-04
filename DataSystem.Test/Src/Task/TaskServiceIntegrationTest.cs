namespace DataSystem.Test.Src.Task
{
    using DataSystem.Application.Task.Business;
    using DataSystem.Application.Task.Service;
    using DataSystem.Infraestructure.Src.Task.Repository;
    using DataSystem.Infraestructure.UnityOfWork;
    using DataSystem.Shared.DTOs.Task;
    using DataSystem.Test.Src.Task.Data;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class TaskServicesTests : IntegrationTestBase
    {
        private readonly TaskServices _taskServices;

        public TaskServicesTests()
        {
            var taskBusiness = new TaskBusiness(Mapper, new TaskRepository(DbContext), new LoggerFactory().CreateLogger<TaskBusiness>());
            _taskServices = new TaskServices(taskBusiness, new WorkUnit(DbContext), Mapper, new LoggerFactory().CreateLogger<TaskServices>());
        }

        [Theory]
        [MemberData(nameof(TaskTestData.GetValidTaskCreateDtos), MemberType = typeof(TaskTestData))]
        public async Task Create_ShouldCreateTask_WhenValid(TaskCreateDTO dto)
        {
            var result = await _taskServices.Create(dto);

            Assert.True(result.Success);
            Assert.NotNull(result.GenericData);
            Assert.Equal(dto.Title, result.GenericData.Title);
        }

        [Fact]
        public async Task Create_ShouldFail_WhenTitleIsMissing()
        {
            var dto = new TaskCreateDTO
            {
                Title = "",
                Description = "Some Description",
                CreatedAt = DateTime.UtcNow,
                Status = Domain.Enums.TaskStatusEnum.Pending
            };

            var result = await _taskServices.Create(dto);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldFail_WhenCompletedAtBeforeCreatedAt()
        {
            var dto = new TaskCreateDTO
            {
                Title = "Teste Inválido",
                CreatedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow.AddDays(-1),
                Status = Domain.Enums.TaskStatusEnum.Completed
            };

            var result = await _taskServices.Create(dto);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(TaskTestData.GetValidTaskCreateDtos), MemberType = typeof(TaskTestData))]
        public async Task Read_ShouldReturnTask_WhenExists(TaskCreateDTO dto)
        {
            var created = await _taskServices.Create(dto);
            var read = await _taskServices.Read(1);

            Assert.True(read.Success);
            Assert.Equal(dto.Title, read.GenericData.Title);
        }

        [Theory]
        [MemberData(nameof(TaskTestData.GetValidTaskCreateDtos), MemberType = typeof(TaskTestData))]
        public async Task Update_ShouldModifyTask_WhenValid(TaskCreateDTO dto)
        {
            var created = await _taskServices.Create(dto);

            var updateDto = new TaskUpdateDTO
            {
                Id = 1,
                Title = "Atualizado",
                Description = dto.Description,
                CreatedAt = dto.CreatedAt,
                CompletedAt = null,
                Status = Domain.Enums.TaskStatusEnum.InProgress
            };

            var result = await _taskServices.Update(updateDto);

            Assert.True(result.Success);
            Assert.Equal("Atualizado", result.GenericData.Title);
        }

        [Theory]
        [MemberData(nameof(TaskTestData.GetValidTaskCreateDtos), MemberType = typeof(TaskTestData))]
        public async Task Delete_ShouldRemoveTask_WhenExists(TaskCreateDTO dto)
        {
            var created = await _taskServices.Create(dto);
            var deleteResult = await _taskServices.Delete(1);

            Assert.True(deleteResult.Success);
            Assert.True(deleteResult.GenericData);

            var read = await _taskServices.Read(created.GenericData.Id);
            Assert.False(read.Success);
            Assert.Equal(404, read.StatusCode);
        }

        [Fact]
        public async Task List_ShouldReturnPaginatedResults()
        {
            var result = await _taskServices.List(new TaskListFilterDTO { PageIndex = 1, PageSize = 10 });

            Assert.True(result.ServiceResponseDTO.Success);
            Assert.NotNull(result.ServiceResponseDTO.GenericData);
        }

        [Fact]
        public async Task Create_ShouldFail_WhenTitleExceedsMaxLength()
        {
            var dto = new TaskCreateDTO
            {
                Title = new string('A', 101), // 101 caracteres
                Description = "Descrição válida",
                CreatedAt = DateTime.UtcNow,
                Status = Domain.Enums.TaskStatusEnum.Pending
            };

            var result = await _taskServices.Create(dto);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("Titulo não pode ter mais de 100 caracteres", result.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Create_ShouldFail_WhenDescriptionExceedsMaxLength()
        {
            var dto = new TaskCreateDTO
            {
                Title = "Título válido",
                Description = new string('D', 1001), // 1001 caracteres
                CreatedAt = DateTime.UtcNow,
                Status = Domain.Enums.TaskStatusEnum.Pending
            };

            var result = await _taskServices.Create(dto);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("Descrição não pode ter mais de 1000 caracteres", result.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Create_ShouldFail_WhenStatusIsCompletedAndCompletedAtIsNull()
        {
            var dto = new TaskCreateDTO
            {
                Title = "Tarefa Finalizada Sem Data",
                Description = "Teste para tarefa com status Completed mas sem CompletedAt",
                CreatedAt = DateTime.UtcNow,
                CompletedAt = null,
                Status = Domain.Enums.TaskStatusEnum.Completed
            };

            var result = await _taskServices.Create(dto);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("conclusão não pode ser nula", result.Message, StringComparison.OrdinalIgnoreCase);
        }


    }
}