using SIDISTraceChecker.Services;
using SIDISTraceChecker.Stores;
using SIDISTraceChecker.ViewModels;

namespace SIDISTraceChecker.Commands
{
    internal class BackToSelectFileCommand : BaseCommand
    {
        private readonly NavigateService<TraceFileViewModel> _navigateService;

        public BackToSelectFileCommand(NavigationStore navigationStore)
        {
            _navigateService = new NavigateService<TraceFileViewModel>(navigationStore, () => new TraceFileViewModel(navigationStore));
        }

        public override void Execute(object parameter)
        {
            _navigateService.Navigate();
        }
    }
}
