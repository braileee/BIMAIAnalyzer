using BIMAIAnalyzer.Core;
using BIMAIAnalyzer.Gemini.Connection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BIMAIAnalyzer.WinApp.ViewModels
{
    public class MainViewViewModel : BindableBase
    {
        public MainViewViewModel(AnalyzeViewViewModel analyzeViewViewModel)
        {
            AnalyzeViewViewModel = analyzeViewViewModel;
        }

        public AnalyzeViewViewModel AnalyzeViewViewModel { get; }
    }
}
