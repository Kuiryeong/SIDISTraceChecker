using SIDISTraceChecker.Services;
using SIDISTraceChecker.Stores;
using SIDISTraceChecker.ViewModels;

namespace SIDISTraceChecker.Commands
{
    internal class RestorePageCommand : BaseCommand
    {
        private readonly TraceDetailViewModel _traceDetailViewModel;

        private readonly NavigateService<TraceFileViewModel> _navigateService;

        public RestorePageCommand(TraceDetailViewModel traceDetailViewModel, NavigationStore navigationStore)
        {
            _traceDetailViewModel = traceDetailViewModel;

            _navigateService = new NavigateService<TraceFileViewModel>(navigationStore, () => _traceDetailViewModel.BackupModel);
        }

        public override void Execute(object parameter)
        {
            _navigateService.Navigate();
        }
    }
}
