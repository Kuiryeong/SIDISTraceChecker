using SIDISTraceChecker.ViewModels;
using System.ComponentModel;

namespace SIDISTraceChecker.Stores
{
    internal class NavigationStore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
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
