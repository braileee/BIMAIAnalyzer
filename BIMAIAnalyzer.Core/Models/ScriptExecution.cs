using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace BIMAIAnalyzer.Core.Models
{
    public class ScriptExecution
    {
        public ScriptExecution(string code)
        {
            Code = code;
        }

        public string Code { get; }

        public async Task<Tout> RunWithReturnValue<T, Tout>(T elementCollection)
        {
            string output = string.Empty;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                               .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                               .Select(a => a.Location)
                               .ToArray();

            var scriptOptions = ScriptOptions.Default
                .AddReferences(assemblies);

            Tout totalValue = default;

            try
            {
                Globals<T> globals = new Globals<T>
                {
                    ElementCollection = elementCollection
                };

                totalValue = await CSharpScript.EvaluateAsync<Tout>(Code, scriptOptions, globals, typeof(Globals<T>));
            }
            catch (Exception exception)
            {
            }

            return totalValue;
        }

        public async Task<ScriptResult> Run()
        {
            // Run the script
            string report = string.Empty;

            try
            {

                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                                           .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                                                           .ToArray();

                // Create script options with references to all assemblies
                var scriptOptions = ScriptOptions.Default
                    .AddReferences(assemblies.Select(a => a.Location))
                    .AddImports("System", "System.Linq", "System.Collections.Generic");

                using (var assemblyLoader = new Microsoft.CodeAnalysis.Scripting.Hosting.InteractiveAssemblyLoader())
                {
                    foreach (var assembly in assemblies)
                    {
                        assemblyLoader.RegisterDependency(assembly);
                    }

                    var script = CSharpScript.Create(Code, scriptOptions, assemblyLoader: assemblyLoader);

                    await script.RunAsync();
                    report = "Code Execution: OK";
                }
            }
            catch (Exception exception)
            {
                report = $"Error: {exception.Message}{Environment.NewLine}{exception.StackTrace}";
            }

            return new ScriptResult
            {
                Report = report
            };
        }
    }
}
