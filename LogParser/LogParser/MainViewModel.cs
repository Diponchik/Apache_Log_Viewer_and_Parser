using System;
using System.ComponentModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using LogParser.Commands;
using LogParser.Services.Interfaces;

namespace LogParser
{
    internal class MainViewModel : Window, INotifyPropertyChanged
    {
        private readonly ILogParserService logParserService;

        private string filePath = "";
        private bool isParseButtonEnable;
        private string parseStatus;

        public bool IsParseButtonEnable
        {
            get => isParseButtonEnable;
            set
            {
                isParseButtonEnable = value;
                RaisePropertyChanged("IsParseButtonEnable");
            }
        }

        public string LabelContent
        {
            get => $"File path:{filePath}";
            set
            {
                filePath = value;
                RaisePropertyChanged("LabelContent");
            }
        }

        public string ParseStatus
        {
            get => parseStatus;
            set
            {
                parseStatus = value;
                RaisePropertyChanged("ParseStatus");
            }
        }

        public MainViewModel(ILogParserService logParserService)
        {
            this.logParserService = logParserService;
        }

        private RelayCommand startParsing;
        private RelayCommand chooseFile;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand StartParsing
        {
            get
            {
                return startParsing ??
                  (startParsing = new RelayCommand(async obj =>
                  {
                      await StartParsingProcess();
                  }));
            }
        }

        public RelayCommand ChooseFile
        {
            get
            {
                return chooseFile ??
                       (chooseFile = new RelayCommand(obj =>
                       {
                           ChooseFileProcess();
                       }));
            }
        }

        public async Task StartParsingProcess()
        {
            ParseStatus = "Parse started";
            IsParseButtonEnable = false;

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    ParseStatus = "Parse in process...";
                    await logParserService.ParseAsync(filePath);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something is going wrong");
            }

            stopWatch.Stop();

            IsParseButtonEnable = true;
            ParseStatus = "Parse finished";
        }

        public void ChooseFileProcess()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            LabelContent = fileDialog.FileName;

            IsParseButtonEnable = !string.IsNullOrEmpty(fileDialog.FileName);
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler == null)
                return;

            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
    }
}
