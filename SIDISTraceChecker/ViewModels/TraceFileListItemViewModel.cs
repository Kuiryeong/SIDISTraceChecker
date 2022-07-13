using SIDISTraceChecker.Models;

namespace SIDISTraceChecker.ViewModels
{
    internal class TraceFileListItemViewModel : ViewModelBase
    {
        private TraceFileListItemModel? _traceFileModel;
        public TraceFileListItemModel TraceFileModel
        {
            get
            {
                return _traceFileModel ?? new TraceFileListItemModel() ;
            }
            set
            {
                _traceFileModel = value;
                OnPropertyChanged(nameof(TraceFileModel));
            }
        }

        public TraceFileListItemViewModel(TraceFileModel traceFileModel)
        {
            TraceFileModel = new TraceFileListItemModel() { FileName = traceFileModel.FileName, StartTime = traceFileModel.StartTime };
        }
    }
}
