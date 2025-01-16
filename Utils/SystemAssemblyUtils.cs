using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIMAIAnalyzer.Utils
{
    public static class SystemAssemblyUtils
    {
        public static void LoadIfNotLoaded(string assemblyPath)
        {
            Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<string> loadedAssemblyFileNames = loadedAssemblies.Select(item => Path.GetFileName(item.Location)).ToList();

            if (!loadedAssemblyFileNames.Contains(Path.GetFileName(assemblyPath)))
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
            }
        }
    }
}
