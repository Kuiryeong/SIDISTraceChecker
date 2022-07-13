using SIDISTraceChecker.Stores;
using SIDISTraceChecker.ViewModels;
using System.Windows;
using System.Windows.Navigation;
using SIDISTraceChecker.Services;
using System.ComponentModel;

namespace SIDISTraceChecker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new TraceFileViewModel(_navigationStore);

            

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
