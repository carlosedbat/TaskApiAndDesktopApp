using AutoMapper;
using DataSystem.Domain.Task.Repository;
using DataSystem.Infraestructure.Context;
using DataSystem.Infraestructure.Src.Task.Repository;
using DataSystem.Shared.MappingProfiles.Task;
using Microsoft.EntityFrameworkCore;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly AppDbContext DbContext;
    protected readonly ITaskRepository TaskRepository;
    protected readonly IMapper Mapper;

    private readonly string _databaseName;

    protected IntegrationTestBase()
    {
        _databaseName = $"DataSystemTestDb_{Guid.NewGuid()}";
        var connectionString = $"Server=(localdb)\\mssqllocaldb;Database={_databaseName};Trusted_Connection=True;MultipleActiveResultSets=True";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        DbContext = new AppDbContext(options);
        DbContext.Database.EnsureCreated();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TaskProfile>();
        });

        Mapper = config.CreateMapper();
        TaskRepository = new TaskRepository(DbContext);
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}
