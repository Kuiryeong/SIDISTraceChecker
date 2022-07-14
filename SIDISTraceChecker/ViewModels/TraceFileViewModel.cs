using SIDISTraceChecker.Commands;
using SIDISTraceChecker.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace SIDISTraceChecker.ViewModels
{
    internal class TraceFileViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;


        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private bool _timeStampFilter;
        public bool TimeStampFilter
        {
            get
            {
                return _timeStampFilter;
            }
            set
            {
                _timeStampFilter = value;
                OnPropertyChanged(nameof(TimeStampFilter));
            }
        }

        private bool _includeTextFilter;
        public bool IncludeTextFilter
        {
            get
            {
                return _includeTextFilter;
            }
            set
            {
                _includeTextFilter = value;
                OnPropertyChanged(nameof(IncludeTextFilter));
            }
        }

        private bool _regexFilter;
        public bool RegexFilter
        {
            get
            {
                return _regexFilter;
            }
            set
            {
                _regexFilter = value;
                OnPropertyChanged(nameof(RegexFilter));
            }
        }

        private string _startTime = DateTime.Now.ToString("yy-MM-dd hh:mm:ss,fff");
        public string StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        private string _endTime = DateTime.Now.ToString("yy-MM-dd hh:mm:ss,fff");
        public string EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }

        private string _includedString;
        public string IncludedString
        {
            get
            {
                return _includedString;
            }
            set
            {
                _includedString = value;
                OnPropertyChanged(nameof(IncludedString));
            }
        }

        private string _regexString;
        public string RegexString
        {
            get
            {
                return _regexString;
            }
            set
            {
                _regexString = value;
                OnPropertyChanged(nameof(RegexString));
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }


        private TraceFileListItemViewModel _selectedFileModel;
        public TraceFileListItemViewModel SelectedFileModel
        {
            get
            {
                return _selectedFileModel;
            }
            set
            {
                _selectedFileModel = value;
                OnPropertyChanged(nameof(SelectedFileModel));

                StartTime = _selectedFileModel.TraceFileModel.StartTime;
                EndTime = _selectedFileModel.TraceFileModel.StartTime;
            }
        }

        private ObservableCollection<TraceFileListItemViewModel> _traceFileModels;
        public ObservableCollection<TraceFileListItemViewModel> TraceFileModels
        {
            get => _traceFileModels;

            set
            {
                _traceFileModels = value;
            }
        }


        public ICommand SelectFilePathCommand { get; }
        public ICommand GoToDetailCommand { get; }


        public TraceFileViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            TraceFileModels = new ObservableCollection<TraceFileListItemViewModel>();

            SelectFilePathCommand = new SelectFilePathCommand(this);
            GoToDetailCommand = new Command(GoToDetail);
        }

        private void GoToDetail()
        {
            if (SelectedFileModel is null)
            {
                MessageBox.Show("Please select a file to check.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SelectedFileModel.TraceFileModel.StartTime == "NotFomattedFile")
            {
                MessageBox.Show("This file seems not SIDIS trace file.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TimeStampFilter)
            {
                try
                {
                    DateTime.ParseExact(StartTime, "yy-MM-dd HH:mm:ss,fff", null);
                    DateTime.ParseExact(EndTime, "yy-MM-dd HH:mm:ss,fff", null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please match the timestamp format.\nto\nyy-MM-dd HH:mm:ss,fff", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (RegexFilter)
            {
                if (string.IsNullOrEmpty(RegexString))
                {
                    MessageBox.Show("Please insert filter string.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (IncludeTextFilter)
            {
                if (string.IsNullOrEmpty(IncludedString))
                {
                    MessageBox.Show("Please insert filter string.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            new GoToDetailCommand(_navigationStore,
                new SelectedFileStore(this)
                {
                    FilePath = this.FilePath,
                    TimeStampFilter = this.TimeStampFilter,
                    IncludeTextFilter = this.IncludeTextFilter,
                    RegexFilter = this.RegexFilter,
                    StartTime = this.StartTime,
                    EndTime = this.EndTime,
                    IncludedString = this.IncludedString,
                    RegexString = this.RegexString,
                    SelectedTraceFileModel = SelectedFileModel
                }).Execute(this);
        }
    }
}
