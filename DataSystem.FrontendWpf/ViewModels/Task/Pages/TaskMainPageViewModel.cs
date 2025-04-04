using DataSystem.Domain.Enums;
using DataSystem.FrontendWpf.Models.Api.DTO;
using DataSystem.FrontendWpf.Models.CrudState.Enum;
using DataSystem.FrontendWpf.Models.Enumerators;
using DataSystem.FrontendWpf.Services.Api.Task;
using DataSystem.FrontendWpf.ViewModels.Task.Windows;
using DataSystem.FrontendWpf.Views.Task.Windows;
using DataSystem.Shared.Constants.Task;
using DataSystem.Shared.DTOs.Task;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DataSystem.FrontendWpf.ViewModels.Task.Pages
{
    public partial class TaskMainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public TaskDTO taskDTO;

        [ObservableProperty]
        private int pageIndex = 1;

        [ObservableProperty]
        private int pageSize = 10;

        [ObservableProperty]
        private ObservableCollection<TaskDTO> tasks;

        [ObservableProperty]
        private TaskDTO selectedTask;

        [ObservableProperty]
        public ObservableCollection<EnumDisplayItem<TaskStatusEnum>> taskStatusFilterOptions;

        [ObservableProperty]
        private EnumDisplayItem<TaskStatusEnum>? selectedStatus;

        private readonly IServiceProvider _serviceProvider;
        private readonly ITaskApi _taskApi;

        public TaskMainPageViewModel(IServiceProvider serviceProvider, ITaskApi taskApi)
        {
            Title = "Task Main Page";

            this._serviceProvider = serviceProvider;
            Tasks = new ObservableCollection<TaskDTO>();

            TaskStatusFilterOptions = new ObservableCollection<EnumDisplayItem<TaskStatusEnum>>(
                Enum.GetValues(typeof(TaskStatusEnum))
                    .Cast<TaskStatusEnum>()
                    .Select(e => new EnumDisplayItem<TaskStatusEnum>(e))
            );

            _taskApi = taskApi;
            LoadTasks();
        }

        [RelayCommand]
        private async void LoadTasks()
        {
            TaskListFilterDTO taskListFilter = new TaskListFilterDTO
            {
                Status = SelectedStatus?.Value
            };

            ApiListResponseWrapperDTO<TaskDTO> listOfTasks = await _taskApi.ListAsync(taskListFilter);

            if(listOfTasks.ServiceResponseDTO.Success == false)
            {
                MessageBox.Show(listOfTasks.ServiceResponseDTO.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Tasks = new ObservableCollection<TaskDTO>(listOfTasks?.ServiceResponseDTO?.GenericData);
        }

        [RelayCommand]
        private void AddTask()
        {
            var detailWindow = _serviceProvider.GetRequiredService<TaskDetailWindow>();
            var viewModel = _serviceProvider.GetRequiredService<TaskDetailWindowViewModel>();

            viewModel.TaskObject = new TaskDTO()
            {
                CreatedAt = DateTime.UtcNow,
            };
            viewModel.TaskCrudState = EnumCrudState.Create;
            viewModel.TaskUpdated += OnTaskUpdated;
            detailWindow.DataContext = viewModel;
            detailWindow.ShowDialog();
        }

        [RelayCommand]
        private void EditTask()
        {
            if (SelectedTask == null) return;

            OpenDetailWindow(SelectedTask);
        }

        private void OpenDetailWindow(TaskDTO taskDTO)
        {
            var detailWindow = _serviceProvider.GetRequiredService<TaskDetailWindow>();
            var viewModel = _serviceProvider.GetRequiredService<TaskDetailWindowViewModel>();

            viewModel.TaskObject = taskDTO;
            viewModel.TaskCrudState = EnumCrudState.Update;
            viewModel.TaskUpdated += OnTaskUpdated;
            detailWindow.DataContext = viewModel;
            detailWindow.ShowDialog();
        }

        [RelayCommand]
        private async void DeleteTask()
        {
            try
            {
                if (SelectedTask == null)
                    return;

                var result = MessageBox.Show(
                    $"Tem certeza de que deseja excluir a tarefa '{SelectedTask.Title}'?",
                    "Confirmação de Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ApiResponseDTO<bool> response = await _taskApi.DeleteAsync(SelectedTask.Id);

                    if(response.Success == false)
                    {
                        MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    LoadTasks();
                    MessageBox.Show(TaskMessagesConst.TaskSuccessDelete, "Exclusão Completa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnTaskUpdated()
        {
            LoadTasks();

            if (_serviceProvider.GetRequiredService<TaskDetailWindowViewModel>() is TaskDetailWindowViewModel viewModel)
            {
                viewModel.TaskUpdated -= OnTaskUpdated;
            }
        }
    }
}
