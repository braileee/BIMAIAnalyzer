﻿using BIMAIAnalyzer.Core;
using BIMAIAnalyzer.Gemini.Connection;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace BIMAIAnalyzer.WinApp.ViewModels
{
    public class AnalyzeViewViewModel : BindableBase
    {
        public AnalyzeViewViewModel()
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
                string apiKey = config[Constants.ApiKeyName];
                PromptMessage promptRequest = new PromptMessage(Constants.GeminiUrlMessage, apiKey);
                ResponseMessage responseMessage = promptRequest.GetResponse(Input);
                Output = responseMessage.Output;
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
