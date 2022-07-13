using Microsoft.Win32;
using SIDISTraceChecker.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace SIDISTraceChecker.Commands
{
    internal class OpenFileCommand : CommandBase
    {
        private readonly TraceDetailViewModel _traceDetailViewModel;

        public OpenFileCommand(TraceDetailViewModel traceDetailViewModel)
        {
            _traceDetailViewModel = traceDetailViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                int lineNo = _traceDetailViewModel.SelectedTimeIndex < 0 ? 1 : _traceDetailViewModel.TraceDetailModels[_traceDetailViewModel.SelectedTimeIndex].LineNo;

                var nppDir = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Notepad++", null, null);
                if (nppDir != null)
                {
                    var nppExePath = Path.Combine(nppDir, "Notepad++.exe");
                    var nppReadmePath = _traceDetailViewModel.SelectedListItemFile.FileName;
                    var line = lineNo;
                    var sb = new StringBuilder();
                    sb.AppendFormat("\"{0}\" -n{1}", nppReadmePath, line);
                    Process.Start(nppExePath, sb.ToString());
                }
                else
                {
                    //string nppExePath = @"C:\Program Files\Notepad++\notepad++.exe";
                    //var nppReadmePath = _traceDetailViewModel.SelectedListItemFile.FileName;
                    //var line = lineNo;
                    //var sb = new StringBuilder();
                    //sb.AppendFormat("\"{0}\" -n{1}", nppReadmePath, line);
                    //Process.Start(nppExePath, sb.ToString());

                    Process.Start("notepad.exe", _traceDetailViewModel.SelectedListItemFile.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
