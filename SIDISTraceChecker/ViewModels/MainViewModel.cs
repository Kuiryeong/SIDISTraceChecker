using SIDISTraceChecker.Stores;

namespace SIDISTraceChecker.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel()
        {
            _navigationStore = new NavigationStore();

            _navigationStore.CurrentViewModel = new TraceFileViewModel(_navigationStore);

            _navigationStore.PropertyChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public override void Dispose()
        {
            _navigationStore.PropertyChanged -= OnCurrentViewModelChanged;

            base.Dispose();
        }
    }
}
