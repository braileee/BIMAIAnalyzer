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
        public MainViewViewModel(AnalyzeViewViewModel analyzeViewViewModel, TuneViewViewModel tuneViewViewModel)
        {
            AnalyzeViewViewModel = analyzeViewViewModel;
            TuneViewViewModel = tuneViewViewModel;
        }

        public AnalyzeViewViewModel AnalyzeViewViewModel { get; }
        public TuneViewViewModel TuneViewViewModel { get; }
    }
}
