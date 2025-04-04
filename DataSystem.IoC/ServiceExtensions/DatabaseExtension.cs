using DataSystem.Infraestructure.Context;
using DataSystem.Shared.Helpers.Environments;
using DataSystem.Shared.Helpers.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DataSystem.IoC.ServiceExtensions
{
    public static class DatabaseExtension
    {
        public static string ConnectionString(IConfiguration iConfiguration)
        {
            EnvironmentVariablesDTO envVars = EnvironmentVariablesHelper.GetEnvironmentVariable();

            string connectionString = iConfiguration.GetConnectionString("DefaultConnection");

            return connectionString;
        }

        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration iConfiguration)
        {
            services.AddHealthChecks()
                .AddSqlServer(ConnectionString(iConfiguration), name: "DataBaseSqlServer");

            return services;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration iConfiguration)
        {
            EnvironmentVariablesDTO envVars = EnvironmentVariablesHelper.GetEnvironmentVariable();
            LogLevel logLevel = DatabaseLogLevelHelper.GetLogLevel(envVars.DatabaseLogLevel);

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<AppDbContext>>();

                options.UseSqlServer(ConnectionString(iConfiguration),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(0))
#if RELEASE
                    .EnableSensitiveDataLogging(false)
                    .EnableDetailedErrors(false)
#else
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true)
#endif
                    .LogTo(message => logger?.Log(logLevel, message), logLevel);
            });

            return services;
        }
    }
}
