using SIDISTraceChecker.Stores;

namespace SIDISTraceChecker.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;   

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.PropertyChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
