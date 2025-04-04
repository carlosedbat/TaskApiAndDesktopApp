using DataSystem.FrontendWpf.Services;
using DataSystem.FrontendWpf.ViewModels.Pages;
using DataSystem.FrontendWpf.ViewModels.Task.Pages;
using DataSystem.FrontendWpf.ViewModels.Task.Windows;
using DataSystem.FrontendWpf.ViewModels.Windows;
using DataSystem.FrontendWpf.Views.Pages;
using DataSystem.FrontendWpf.Views.Task.Pages;
using DataSystem.FrontendWpf.Views.Task.Windows;
using DataSystem.FrontendWpf.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;

namespace DataSystem.FrontendWpf.ServiceExtensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            // Registre ApplicationHostService como singleton e hosted service
            services.AddSingleton<ApplicationHostService>();
            services.AddHostedService(sp => sp.GetRequiredService<ApplicationHostService>());


            // Page resolver service
            services.AddSingleton<IPageService, PageService>();

            // Theme manipulation
            services.AddSingleton<IThemeService, ThemeService>();

            // TaskBar manipulation
            services.AddSingleton<ITaskBarService, TaskBarService>();

            // Service containing navigation, same as INavigationWindow... but without window
            services.AddSingleton<INavigationService, NavigationService>();

            // Main window with navigation
            services.AddSingleton<INavigationWindow, MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<SettingsPage>();
            services.AddSingleton<SettingsViewModel>();

            services.AddTransient<TaskMainView>();
            services.AddScoped<TaskMainPageViewModel>();

            services.AddTransient<TaskDetailWindow>();
            services.AddScoped<TaskDetailWindowViewModel>();

            return services;
        }
    }
}
