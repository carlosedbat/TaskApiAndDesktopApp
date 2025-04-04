namespace DataSystem.FrontendWpf.ServiceExtensions
{
    using DataSystem.FrontendWpf.Helpers.Environment;
    using DataSystem.FrontendWpf.Services.Api.Task;
    using Microsoft.Extensions.DependencyInjection;
    using Refit;
    using System;
    using System.Net.Http;
    using System.Text.Json.Serialization;
    using System.Text.Json;

    public static class ApiExtension
    {
        public static IServiceCollection ConfigureApi(this IServiceCollection services)
        {
            EnvironmentFrontendVariablesDTO environmentVariables = EnvironmentHelper.Variables;

            var baseAddress = new Uri(environmentVariables.BackendApi);

            services.AddRefitClientWithSsl<ITaskApi>(baseAddress);

            return services;
        }

        public static IServiceCollection AddRefitClientWithSsl<TInterface>(this IServiceCollection services, Uri baseAddress)
           where TInterface : class
        {

            var settings = new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                })
            };
#if DEBUG
            services.AddRefitClient<TInterface>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = baseAddress)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });
#else
            services.AddRefitClient<TInterface>(settings)
                            .ConfigureHttpClient(c => c.BaseAddress = baseAddress);
#endif
            return services;
        }
    }
}
