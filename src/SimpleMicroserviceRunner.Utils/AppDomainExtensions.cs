using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SimpleMicroserviceRunner.Utils
{
    public static class AppDomainExtensions
    {
        public static IEnumerable<Assembly> GetAllAssemblies(this AppDomain domain)
        {
            // Had some issues using DependencyContext approach, therefore resorted to loading all assemblies using the executing assembly location.
            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<Assembly> assemblies = new List<Assembly>();
            var files = new DirectoryInfo(assemblyDirectory).GetFiles();

            files
                .Where(file => file.Extension.ToUpperInvariant() == ".DLL")
                .ForEach(file =>
                {
                    try
                    {
                        assemblies.Add(Assembly.Load(AssemblyName.GetAssemblyName(file.FullName)));
                    }
                    catch
                    {
                        // supress any assembly load errors.
                    }
                });

            return domain.GetAssemblies().Distinct();
        }
    }
}
