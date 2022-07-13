using SIDISTraceChecker.ViewModels;
using System.ComponentModel;

namespace SIDISTraceChecker.Stores
{
    internal class NavigationStore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
