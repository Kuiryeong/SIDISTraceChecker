using SIDISTraceChecker.Models;
using SIDISTraceChecker.ViewModels;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace SIDISTraceChecker.Commands
{
    internal class SelectFilePathCommand : BaseCommand
    {
        private string _filePath;

        private readonly TraceFileViewModel _traceFileViewModel;

        public SelectFilePathCommand(TraceFileViewModel traceFileViewModel)
        {
            _traceFileViewModel = traceFileViewModel;
        }

        public override async void Execute(object parameter)
        {
            using (System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult resDial = fbd.ShowDialog();
                if (resDial == System.Windows.Forms.DialogResult.OK)
                {
                    _filePath = fbd.SelectedPath;
                }
                else
                {
                    return;
                }
            }

            _traceFileViewModel.FilePath = _filePath;

            _traceFileViewModel.IsLoading = true;

            _traceFileViewModel.TraceFileModels?.Clear();

            await Task.Factory.StartNew(() => { GetFiles(_filePath); });

            _traceFileViewModel.IsLoading = false;
        }

        public void GetFiles(string filePath)
        {
            string[] files = Directory.GetFiles(filePath, "*.tr*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Application.Current.Dispatcher.Invoke(() =>
                _traceFileViewModel?.TraceFileModels?.Add(new TraceFileListItemViewModel(new TraceFileModel() { FileName = file })));
            }
        }
    }
}
