using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using SimpleMicroserviceRunner.Utils;

namespace SimpleMicroserviceRunner.Plugin.DependencyInjection
{
    public class ContainerWrapper : IDisposable
    {
        public static Container Container { get; } = new Container();

        public static bool IsContainerInitialised { get; private set; } = false;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Initialise(Func<AssemblyName, bool> assemblyFilter = null)
        {
            try
            {
                // Gets all exported types from the assemblies loaded in the appdomain
                var types = this.GetTypes(assemblyFilter ?? this.FallBackAssemblyFilter);

                var containerRegistries = types.Where(type => type.Implements<IContainerRegistry>()).Select(type => Activator.CreateInstance(type));

                foreach (IContainerRegistry containerRegistry in containerRegistries)
                {
                    containerRegistry.Register(Container);
                }

                Container.Verify();
                IsContainerInitialised = true;
            }
            catch (Exception)
            {
                IsContainerInitialised = false;
                throw;
            }
        }

        private List<Type> GetTypes(Func<AssemblyName, bool> assemblyFilter)
        {
            var types = new List<Type>();

            AppDomain.CurrentDomain.GetAllAssemblies()
                .Where(assembly => assemblyFilter(assembly.GetName()))
                .ForEach(assembly =>
                {
                    types.AddRange(assembly.GetExportedTypes());
                });

            return types;
        }

        private bool FallBackAssemblyFilter(AssemblyName assemblyName)
        {
            return true;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                IsContainerInitialised = false;
                Container.Dispose();
            }
        }
    }
}
