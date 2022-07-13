using SIDISTraceChecker.Services;
using SIDISTraceChecker.Stores;
using SIDISTraceChecker.ViewModels;

namespace SIDISTraceChecker.Commands
{
    internal class BackToSelectFileCommand : CommandBase
    {
        private readonly TraceDetailViewModel _traceDetailViewModel;

        private readonly NavigateService<TraceFileViewModel> _navigateService;

        public BackToSelectFileCommand(TraceDetailViewModel traceDetailViewModel, NavigationStore navigationStore)
        {
            _traceDetailViewModel = traceDetailViewModel;

            _navigateService = new NavigateService<TraceFileViewModel>(navigationStore, () => new TraceFileViewModel(navigationStore));
        }

        public override void Execute(object parameter)
        {
            _navigateService.Navigate();
        }
    }
}
