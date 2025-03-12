using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.Runtime;
using Autofac;
using BIMAIAnalyzer.Civil3D.Models;
using BIMAIAnalyzer.Civil3D.Utils;
using BIMAIAnalyzer.Civil3D.Views;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Documents;

namespace BIMAIAnalyzer.Civil3D
{
    public class Main
    {

        [CommandMethod("PSV", "BIMAIAnalyzer", CommandFlags.Modal)]
        public static void Start()
        {
            try
            {
                string assemblyDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Main)).Location);
                string mahappsMetroDllPath = Path.Combine(assemblyDirectory, "MahApps.Metro.dll");

                SystemAssemblyUtils.LoadIfNotLoaded(mahappsMetroDllPath);

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
