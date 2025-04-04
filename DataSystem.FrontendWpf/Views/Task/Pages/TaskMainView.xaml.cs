using DataSystem.FrontendWpf.ViewModels.Task.Pages;
using System.Windows.Controls;

namespace DataSystem.FrontendWpf.Views.Task.Pages
{
    /// <summary>
    /// Interação lógica para TaskMainView.xam
    /// </summary>
    public partial class TaskMainView : Page
    {
        public TaskMainPageViewModel ViewModel { get; set; }
        public TaskMainView(TaskMainPageViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;

            InitializeComponent();
        }

        private void StatusFilter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is TaskMainPageViewModel vm)
            {
                vm.LoadTasksCommand.Execute(null);
            }
        }
    }
}
