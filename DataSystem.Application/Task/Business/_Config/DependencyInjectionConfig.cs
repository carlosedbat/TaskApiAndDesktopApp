namespace DataSystem.Application.Task.Business._Config
{

    #region USINGS

    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class DependencyInjectionConfig
    {
        public static void RegisterServicesInjection(IServiceCollection services)
        {
            services.AddScoped<ITaskBusiness, TaskBusiness>();
        }
    }

}
