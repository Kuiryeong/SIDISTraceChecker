using SIDISTraceChecker.Services;
using SIDISTraceChecker.Stores;
using SIDISTraceChecker.ViewModels;

namespace SIDISTraceChecker.Commands
{
    internal class GoToDetailCommand : BaseCommand
    {
        private readonly NavigateService<TraceDetailViewModel> _navigateService;

        public GoToDetailCommand(NavigationStore navigationStore, SelectedFileStore selectedFileStore)
        {
            _navigateService = new NavigateService<TraceDetailViewModel>(navigationStore, () => new TraceDetailViewModel(navigationStore, selectedFileStore));
        }

        public override void Execute(object parameter)
        {
            _navigateService.Navigate();
        }
    }
}
