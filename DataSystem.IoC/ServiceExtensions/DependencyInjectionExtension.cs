using DataSystem.Application.Task.Service;
using DataSystem.Domain.UnitOfWork;
using DataSystem.Infraestructure.UnityOfWork;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

namespace DataSystem.IoC.ServiceExtensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ConfigureBaseDependency(this IServiceCollection services)
        {
            services.AddScoped<IWorkUnit, WorkUnit>();
            services.AddScoped<ITaskServices, TaskServices>();
            return services;
        }
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                 .Where(a => a.FullName.StartsWith("DataSystem"));

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                                    .Where(t => t.IsClass && t.IsPublic);

                foreach (var type in types)
                {
                    var method = type.GetMethod("RegisterServicesInjection", BindingFlags.Public | BindingFlags.Static);

                    if (method != null)
                    {
                        var parameters = method.GetParameters();
                        if (parameters.Length == 1 && parameters[0].ParameterType == typeof(IServiceCollection))
                        {
                            try
                            {
                                method.Invoke(null, new object[] { services });
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error to register dependencies on class {type.FullName}: {ex.Message}");
                            }
                        }
                    }
                }
            }

            return services;
        }
    }
}
