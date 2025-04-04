namespace DataSystem.Application.Task.Service._Config
{

    #region USINGS

    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class DependencyInjectionConfig
    {
        public static void RegisterServicesInjection(IServiceCollection services)
        {
            //services.AddScoped<ITaskServices, TaskServices>();
        }
    }

}
