namespace DataSystem.Infraestructure.Src.Task.Repository._Config
{
    using DataSystem.Domain.Task.Repository;

    #region USINGS

    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class DependencyInjectionConfig
    {
        public static void RegisterServicesInjection(IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }

}
