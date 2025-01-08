using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.Runtime;
using Autofac;
using BIMAIAnalyzer.Models;
using BIMAIAnalyzer.Utils;
using BIMAIAnalyzer.Views;
using Microsoft.Extensions.Configuration;

namespace BIMAIAnalyzer
{
    public class Main
    {
        public static string ApiKey { get; private set; }

        [CommandMethod("PSV", "BIMAIAnalyzer", CommandFlags.Modal)]
        public static void Start()
        {
            try
            {
                var config = new ConfigurationBuilder().AddUserSecrets<Main>().Build();
                ApiKey = config["apikey"];

                var bootstrapper = new Bootstrapper();
                var container = bootstrapper.Bootstrap();
                var mainView = container.Resolve<MainView>();
                Application.ShowModelessWindow(mainView);
            }
            catch (System.Exception exception)
            {
                MessageBoxUtils.ShowInfo(exception.Message);
            }
        }
    }
}
