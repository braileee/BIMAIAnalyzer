using Prism.Commands;
using Prism.Mvvm;
using System;
using Newtonsoft.Json;
using BIMAIAnalyzer.Civil3D.Models;
using BIMAIAnalyzer.Core;
using BIMAIAnalyzer.Gemini.Connection;
using Microsoft.Extensions.Configuration;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace BIMAIAnalyzer.Civil3D.ViewModels
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

                IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Main>().Build();
                string apiKey = config[Constants.ApiKeyName];

                PromptMessage promptRequest = new PromptMessage(Constants.GeminiUrlMessage, apiKey);
                string inputWithElementData = $"{Input}{Environment.NewLine}{elementJsonContent}";
                ResponseMessage promptResponse = promptRequest.GetResponse(inputWithElementData);

                Output = promptResponse.Output;
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
