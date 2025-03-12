using BIMAIAnalyzer.Civil3D.ViewModels;
using System.Windows;

namespace BIMAIAnalyzer.Civil3D.Views
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
