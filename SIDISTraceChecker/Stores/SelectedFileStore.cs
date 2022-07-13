using SIDISTraceChecker.Models;
using SIDISTraceChecker.ViewModels;
using System;

namespace SIDISTraceChecker.Stores
{
    internal class SelectedFileStore
    {
        public readonly TraceFileViewModel _traceFileViewModel;

        public string FilePath { get; set; }
        public bool TimeStampFilter { get; set; }
        public bool IncludeTextFilter { get; set; }
        public bool RegexFilter { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string IncludedString { get; set; }
        public string RegexString { get; set; }

        private TraceFileListItemViewModel slectedTraceFileModel;

        public SelectedFileStore(TraceFileViewModel traceFileViewModel)
        {
            _traceFileViewModel = traceFileViewModel;
        }

        public TraceFileListItemViewModel SelectedTraceFileModel
        {
            get
            {
                return slectedTraceFileModel;
            }
            set
            {
                slectedTraceFileModel = value;
                SelectedTraceFileUpdated?.Invoke();
            }
        }

        public event Action SelectedTraceFileUpdated;
    }
}
