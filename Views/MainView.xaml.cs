using BIMAIAnalyzer.ViewModels;
using System.Windows;

namespace BIMAIAnalyzer.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewViewModel();
        }
    }
}
