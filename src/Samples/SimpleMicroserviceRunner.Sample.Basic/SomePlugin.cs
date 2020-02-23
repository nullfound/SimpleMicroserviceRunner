using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Runtime.Plugin;

namespace SimpleMicroserviceRunner.Sample.Basic
{
    public class SomePlugin : IPlugin
    {
        public PluginExecutionPriorityEnum PluginExecutionPriority => PluginExecutionPriorityEnum.Lowest;

        public Task RunAfterMicroserviceShutdownAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Calling from RunAfterMicroserviceShutdownAsync");
            return Task.CompletedTask;
        }

        public Task RunBeforeHostShutdownAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Calling from RunBeforeHostShutdownAsync");
            return Task.CompletedTask;
        }

        public Task RunBeforeMicroserviceStartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Calling from RunBeforeMicroserviceStartAsync");
            return Task.CompletedTask;
        }

        public Task RunOnHostInitialisationAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Calling from RunOnHostInitialisationAsync");
            return Task.CompletedTask;
        }

        public Task RunTidyUpAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Calling from RunTidyUpAsync");
            return Task.CompletedTask;
        }
    }
}
