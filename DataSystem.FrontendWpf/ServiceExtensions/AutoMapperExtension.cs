﻿using DataSystem.Shared.MappingProfiles.Task;
using Microsoft.Extensions.DependencyInjection;

namespace DataSystem.FrontendWpf.ServiceExtensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(TaskProfile));
            return services;
        }
    }
}
