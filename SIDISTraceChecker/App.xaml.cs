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
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
