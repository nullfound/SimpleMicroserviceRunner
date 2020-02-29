using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMicroserviceRunner.Plugin.DependencyInjection
{
    public sealed class SimpleInjectorPlugin : IPlugin, IDisposable
    {
        private readonly ContainerWrapper containerWrapper;
        private readonly Func<AssemblyName, bool> assemblyFilter;

        public SimpleInjectorPlugin()
        {
            this.containerWrapper = new ContainerWrapper();
            this.assemblyFilter = null;
        }

        public SimpleInjectorPlugin(Func<AssemblyName, bool> assemblyFilter)
        {
            this.containerWrapper = new ContainerWrapper();
            this.assemblyFilter = assemblyFilter;
        }

        public PluginExecutionPriorityEnum PluginExecutionPriority => PluginExecutionPriorityEnum.Highest;

        public Task RunAfterMicroserviceShutdownAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task RunBeforeHostShutdownAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task RunBeforeMicroserviceStartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task RunOnHostInitialisationAsync(CancellationToken cancellationToken)
        {
            var task = Task.Factory.StartNew(
               () =>
               {
                   this.containerWrapper.Initialise(this.assemblyFilter);
               },
               CancellationToken.None,
               TaskCreationOptions.LongRunning,
               TaskScheduler.Current);

            await task.ConfigureAwait(false);
        }

        public Task RunTidyUpAsync(CancellationToken cancellationToken)
        {
            this.containerWrapper.Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.containerWrapper.Dispose();
            }
        }
    }
}
