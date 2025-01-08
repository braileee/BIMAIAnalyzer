using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIMAIAnalyzer.Utils
{
    public static class MessageBoxUtils
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error");
        }

        public static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info");
        }

        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning");
        }
    }
}
