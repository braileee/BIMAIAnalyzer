using Prism.Commands;
using Prism.Mvvm;
using System;
using BIMAIAnalyzer.Models;
using Newtonsoft.Json;

namespace BIMAIAnalyzer.ViewModels
{
    public class MainViewViewModel : BindableBase
    {
        private string input;
        private string output;

        public MainViewViewModel()
        {
            ProcessInputCommand = new DelegateCommand(OnProcessInputCommand);
        }

        private void OnProcessInputCommand()
        {
            try
            {
                ElementCollection elementCollection = ElementCollection.Create();

                string elementJsonContent = JsonConvert.SerializeObject(elementCollection);

                PromptRequest promptRequest = new PromptRequest(Constants.GeminiUrl, Main.ApiKey);
                string inputWithElementData = $"{Input}{Environment.NewLine}{elementJsonContent}";
                Output = promptRequest.GetResponse(inputWithElementData);
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
