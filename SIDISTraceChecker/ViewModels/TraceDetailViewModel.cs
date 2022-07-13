using SIDISTraceChecker.Commands;
using SIDISTraceChecker.Models;
using SIDISTraceChecker.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;

namespace SIDISTraceChecker.ViewModels
{
    internal class TraceDetailViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly SelectedFileStore _selectedFileStore;


        public string FileName
        {
            get => "File Name :" + SelectedListItemFile.FileName;
        }

        public string HighlightText => _selectedFileStore.IncludedString;

        public TraceFileViewModel BackupModel { get => _selectedFileStore._traceFileViewModel; }

        private int selectedTimeIndex;
        public int SelectedTimeIndex
        {
            get
            {
                return selectedTimeIndex;
            }
            set
            {
                selectedTimeIndex = value;
                OnPropertyChanged(nameof(SelectedTimeIndex));

                if (TraceDetailModels.Count > 0 && selectedTimeIndex >= 0)
                    SelectedDetalString = TraceDetailModels.Select(s => s.FullMessages).ElementAt<string>(selectedTimeIndex);
            }
        }

        public TraceFileListItemModel SelectedListItemFile { get => _selectedFileStore.SelectedTraceFileModel.TraceFileModel; }
        public ObservableCollection<string> Initimes
        {
            get => new ObservableCollection<string>(TraceDetailModels.Select(s => s.IniTime).AsEnumerable<string>());
        }

        private ObservableCollection<TraceDetailModel> traceDetailModels;
        public ObservableCollection<TraceDetailModel> TraceDetailModels
        {
            get
            {
                return traceDetailModels;
            }
            set
            {
                traceDetailModels = value;
            }
        }

        private string selectedDetailString;
        public string SelectedDetalString
        {
            get
            {
                return selectedDetailString;
            }
            set
            {
                selectedDetailString = value;
                OnPropertyChanged(nameof(SelectedDetalString));
            }
        }


        public ICommand BackToSelectFileCommand { get; }
        public ICommand OpenFileCommand { get; }


        public TraceDetailViewModel(NavigationStore navigationStore, SelectedFileStore selectedFileStore)
        {
            traceDetailModels = new ObservableCollection<TraceDetailModel>();

            _navigationStore = navigationStore;

            _selectedFileStore = selectedFileStore;

            BackToSelectFileCommand = new Command(BackToSelectFile);

            OpenFileCommand = new OpenFileCommand(this);

            FilteringData();

            SelectedTimeIndex = 0;
        }

        private void BackToSelectFile()
        {
            DialogResult res = MessageBox.Show("Do you want restore previous page?", "Restore Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.No)
            {
                new BackToSelectFileCommand(this, _navigationStore).Execute(null);
            }
            else
            {
                new RestorePageCommand(this, _navigationStore).Execute(null);
            }
        }

        private void FilteringData()
        {
            string filename = SelectedListItemFile.FileName;

            using (StreamReader sr = new StreamReader(filename))
            {
                int lineNo = 1;

                DateTime iniTime;
                List<string> textSet = new List<string>();

                string[] strings = sr.ReadToEnd().Split(@"--------##");

                for (int i = 0; i < strings.Length; i++)
                {
                    string stringSet = strings[i];
                    int currentLine = lineNo == 1 ? stringSet.Split("\n").Length - 1 : stringSet.Split("\n").Length - 2;

                    if (stringSet.Length < 23)
                    {
                        lineNo = lineNo + currentLine;
                        continue;
                    }

                    if (stringSet.Substring(0, 1) == "\r")
                    {
                        lineNo++;
                        stringSet = new string(stringSet.Skip(2).ToArray());
                    }

                    if (stringSet.Substring(0,1) == "\r") iniTime = DateTime.ParseExact(stringSet.Substring(2, 21), "yy-MM-dd HH:mm:ss,fff", null);
                    else iniTime = DateTime.ParseExact(stringSet.Substring(0, 21), "yy-MM-dd HH:mm:ss,fff", null);

                    if (_selectedFileStore.TimeStampFilter)
                    {
                        if (DateTime.ParseExact(_selectedFileStore.StartTime, "yy-MM-dd HH:mm:ss,fff", null)
                                > iniTime ||
                                iniTime >
                                DateTime.ParseExact(_selectedFileStore.EndTime, "yy-MM-dd HH:mm:ss,fff", null))
                        {
                            lineNo = lineNo + stringSet.Split("\r\n").Length + 1;
                            continue;
                        }
                    }

                    if (_selectedFileStore.IncludeTextFilter)
                    {
                        if (!stringSet.Contains(_selectedFileStore.IncludedString, StringComparison.OrdinalIgnoreCase))
                        {
                            lineNo = lineNo + stringSet.Split("\r\n").Length + 1;
                            continue;
                        }
                    }

                    if (_selectedFileStore.RegexFilter)
                    {
                        if (!Regex.IsMatch(stringSet, _selectedFileStore.RegexString))
                        {
                            lineNo = lineNo + stringSet.Split("\r\n").Length + 1;
                            continue;
                        }
                    }

                    TraceDetailModels.Add(new TraceDetailModel()
                    {
                        FilePath = _selectedFileStore.FilePath,
                        FullMessages = stringSet,
                        IniTime = iniTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        LineNo = lineNo
                    });

                    lineNo = lineNo + currentLine;
                }

            }
        }
    }
}
