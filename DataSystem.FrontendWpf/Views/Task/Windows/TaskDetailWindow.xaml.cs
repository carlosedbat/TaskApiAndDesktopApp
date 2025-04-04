using Wpf.Ui.Appearance;

namespace DataSystem.FrontendWpf.Views.Task.Windows
{
    /// <summary>
    /// Lógica interna para TaskDetailWindow.xaml
    /// </summary>
    public partial class TaskDetailWindow : Window
    {
        public TaskDetailWindow()
        {
            SystemThemeWatcher.Watch(this);
            InitializeComponent();
        }
    }
}
