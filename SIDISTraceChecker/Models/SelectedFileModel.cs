using SIDISTraceChecker.ViewModels;
using System;

namespace SIDISTraceChecker.Models
{
    internal class SelectedFileModel
    {
        public string FilePath { get; set; }
        public bool TimeStampFilter { get; set; }
        public bool IncludeTextFilter { get; set; }
        public bool RegexFilter { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string IncludedString { get; set; }
        public string RegexString { get; set; }

        private TraceFileListItemViewModel traceFileModel;
        public TraceFileListItemViewModel TraceFileModel
        {
            get
            {
                return traceFileModel;
            }
            set
            {
                traceFileModel = value;
                SelectedTraceFileUpdated?.Invoke();
            }
        }

        public event Action SelectedTraceFileUpdated;
    }
}
