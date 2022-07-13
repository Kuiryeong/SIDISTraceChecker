using SIDISTraceChecker.Services;
using SIDISTraceChecker.Stores;
using SIDISTraceChecker.ViewModels;

namespace SIDISTraceChecker.Commands
{
    internal class GoToDetailCommand : CommandBase
    {
        private readonly TraceFileViewModel _traceFileViewModel;

        private readonly NavigateService<TraceDetailViewModel> _navigateService;

        public GoToDetailCommand(TraceFileViewModel traceFileViewModel, NavigationStore navigationStore, SelectedFileStore selectedFileStore)
        {
            _traceFileViewModel = traceFileViewModel;

            _navigateService = new NavigateService<TraceDetailViewModel>(navigationStore, () => new TraceDetailViewModel(navigationStore, selectedFileStore));
        }

        public override void Execute(object parameter)
        {
            _navigateService.Navigate();
        }
    }
}
