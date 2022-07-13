using SIDISTraceChecker.Stores;
using SIDISTraceChecker.ViewModels;
using System;

namespace SIDISTraceChecker.Services
{
    internal class NavigateService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly Func<TViewModel> _viewModelFactory;

        public NavigateService(NavigationStore navigationStore,  Func<TViewModel> viewModelFactory)
        {
            _navigationStore = navigationStore;
            _viewModelFactory = viewModelFactory;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _viewModelFactory();
        }
    }
}
