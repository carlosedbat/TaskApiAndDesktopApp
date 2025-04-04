using DataSystem.IoC.ServiceExtensions;
using Serilog.Events;
using Serilog;
using DataSystem.Shared.Helpers.Log;
using DataSystem.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

int oneGigaByte = 1073741824;

var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
#if DEBUG
    .WriteTo.Debug()
#else
    .WriteTo.Console()
#endif
    .WriteTo.Sink(new CustomFileSizeLimitedSink("logs/debug.txt", oneGigaByte), LogEventLevel.Debug)
    .WriteTo.Sink(new CustomFileSizeLimitedSink("logs/info.txt", oneGigaByte), LogEventLevel.Information)
    .WriteTo.Sink(new CustomFileSizeLimitedSink("logs/warning.txt", oneGigaByte), LogEventLevel.Warning)
    .WriteTo.Sink(new CustomFileSizeLimitedSink("logs/error.txt", oneGigaByte), LogEventLevel.Error)
    .WriteTo.Sink(new CustomFileSizeLimitedSink("logs/fatal.txt", oneGigaByte), LogEventLevel.Fatal)
    .CreateLogger();

// Add services to the container.

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.ConfigureBaseDependency();
builder.Services.ConfigureDependencyInjection();
builder.Services.ConfigureAutomapper();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    ILogger<Program>? loggerMigration = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    if (loggerMigration is null)
    {
        throw new ArgumentNullException(nameof(loggerMigration));
    }

    AppDbContext? context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (context is null)
    {
        throw new ArgumentNullException(nameof(context));
    }

    IEnumerable<string> pendingMigrations = await context.Database.GetPendingMigrationsAsync();

    try
    {
        if (pendingMigrations.Any())
        {
            loggerMigration.LogInformation($"Found {pendingMigrations.Count()} pending migrations for AppDbcontext");
            await context.Database.MigrateAsync();
        }
        else
        {
            loggerMigration.LogInformation("No pending migrations for AppDbContext");
        }
        await context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        loggerMigration.LogError(ex, "Error during database migration process");
        throw;
    }
}

app.Run();
