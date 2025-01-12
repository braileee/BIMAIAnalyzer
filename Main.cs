using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.Runtime;
using Autofac;
using BIMAIAnalyzer.Models;
using BIMAIAnalyzer.Utils;
using BIMAIAnalyzer.Views;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

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
                IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Main>().Build();
                ApiKey = config["apikey"];

                string assemblyDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Main)).Location);
                string mahappsMetroDllPath = Path.Combine(assemblyDirectory, "MahApps.Metro.dll");

                Assembly assembly = Assembly.LoadFrom(mahappsMetroDllPath);

                Bootstrapper bootstrapper = new Bootstrapper();
                IContainer container = bootstrapper.Bootstrap();
                MainView mainView = container.Resolve<MainView>();
                Application.ShowModelessWindow(mainView);
            }
            catch (System.Exception exception)
            {
                MessageBoxUtils.ShowInfo(exception.Message);
            }
        }
    }
}
