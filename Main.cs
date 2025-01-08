using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.Runtime;
using Autofac;
using BIMAIAnalyzer.Models;
using BIMAIAnalyzer.Views;
using Microsoft.Extensions.Configuration;

namespace BIMAIAnalyzer
{
    public class Main
    {
        [CommandMethod("PSV", "BIMAIAnalyzer", CommandFlags.Modal)]
        public static void Start()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Main>().Build();
            string apiKey = config["apikey"];

            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            var mainView = container.Resolve<MainView>();
            Application.ShowModelessWindow(mainView);
        }
    }
}
