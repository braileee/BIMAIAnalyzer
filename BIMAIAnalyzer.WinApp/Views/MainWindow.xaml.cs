using BIMAIAnalyzer.WinApp.ViewModels;
using System.Windows;

namespace BIMAIAnalyzer.WinApp.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AnalyzeViewViewModel analyzeViewViewModel = new AnalyzeViewViewModel();
        TuneViewViewModel tuneViewViewModel = new TuneViewViewModel();
        DataContext = new MainViewViewModel(analyzeViewViewModel, tuneViewViewModel);
    }
}