using BIMAIAnalyzer.Core;
using BIMAIAnalyzer.Gemini.Connection;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAIAnalyzer.WinApp.ViewModels
{
    public class TuneViewViewModel : BindableBase
    {
        public TuneViewViewModel()
        {
            ProcessInputCommand = new DelegateCommand(OnProcessInputCommand);
        }


        private string input;
        private string output;

        private void OnProcessInputCommand()
        {
            try
            {
                IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<MainViewViewModel>().Build();
                string apiKey = config["apikey"];

                Output = promptRequest.GetResponse(Input);
            }
            catch (Exception)
            {
            }
        }

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                input = value;
                RaisePropertyChanged();
            }
        }
        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand ProcessInputCommand { get; }
    }
}
