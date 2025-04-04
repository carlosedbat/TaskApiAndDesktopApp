namespace DataSystem.FrontendWpf.ViewModels.Task.Windows
{
    using AutoMapper;
    using DataSystem.Domain.Enums;
    using DataSystem.FrontendWpf.Models.CrudState.Enum;
    using DataSystem.FrontendWpf.Models.Enumerators;
    using DataSystem.FrontendWpf.Services.Api.Task;
    using DataSystem.Shared.Constants.Task;
    using DataSystem.Shared.DTOs.Task;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class TaskDetailWindowViewModel : ObservableObject
    {
        private ITaskApi _taskApi;
        private IMapper _mapper;

        [ObservableProperty]
        private TaskDTO taskObject;

        [ObservableProperty]
        private EnumCrudState _taskCrudState;

        public ObservableCollection<EnumDisplayItem<TaskStatusEnum>> TaskStatus { get; }

        public event Action TaskUpdated;

        public TaskDetailWindowViewModel(
            ITaskApi taskApi,
            IMapper mapper)
        {
            this._taskApi = taskApi;
            TaskStatus = new ObservableCollection<EnumDisplayItem<TaskStatusEnum>>(
                Enum.GetValues(typeof(TaskStatusEnum))
                    .Cast<TaskStatusEnum>()
                    .OrderBy(e => (int)e)
                    .TakeWhile((e, i) => i < Enum.GetValues(typeof(TaskStatusEnum)).Length - 1)
                    .Select(e => new EnumDisplayItem<TaskStatusEnum>(e))
            );

            this._mapper = mapper;
        }

        private async Task CreateNewTask()
        {
            try
            {
                TaskCreateDTO taskCreateDTO = _mapper.Map<TaskCreateDTO>(TaskObject);

                await this._taskApi.CreateAsync(taskCreateDTO);
                TaskUpdated?.Invoke();
                MessageBox.Show(TaskMessagesConst.TaskSuccessCreate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task UpdateTask()
        {
            try
            {
                TaskUpdateDTO taskUpdateDTO = _mapper.Map<TaskUpdateDTO>(TaskObject);

                await this._taskApi.UpdateAsync(taskUpdateDTO);
                TaskUpdated?.Invoke();
                MessageBox.Show(TaskMessagesConst.TaskSuccessUpdate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [RelayCommand]
        private async void Save()
        {
            try
            {
                if (!IsValidTask(out string error))
                {
                    MessageBox.Show(error, "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (TaskCrudState == EnumCrudState.Create)
                {
                    await this.CreateNewTask();
                }
                else
                {
                    await this.UpdateTask();
                }

                CloseWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(window => window.DataContext == this)
                ?.Close();
        }

        private bool IsValidTask(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (TaskObject.Status == TaskStatusEnum.Completed)
            {
                if (TaskObject.CompletedAt == null)
                {
                    errorMessage = "O campo 'Concluído em' é obrigatório para tarefas concluídas.";
                    return false;
                }

                if (TaskObject.CompletedAt < TaskObject.CreatedAt)
                {
                    errorMessage = "'Concluído em' não pode ser menor que 'Criado em'.";
                    return false;
                }
            }
            else
            {
                if (TaskObject.CompletedAt != null)
                {
                    errorMessage = "'Concluído em' deve estar vazio para tarefas não concluídas.";
                    return false;
                }
            }

            return true;
        }
    }
}
