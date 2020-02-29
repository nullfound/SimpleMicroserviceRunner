using System.Threading;
using System.Threading.Tasks;

namespace SimpleMicroserviceRunner.Plugin
{
    public interface IPlugin
    {
        PluginExecutionPriorityEnum PluginExecutionPriority { get; }

        Task RunOnHostInitialisationAsync(CancellationToken cancellationToken);

        Task RunBeforeMicroserviceStartAsync(CancellationToken cancellationToken);

        Task RunAfterMicroserviceShutdownAsync(CancellationToken cancellationToken);

        Task RunBeforeHostShutdownAsync(CancellationToken cancellationToken);

        Task RunTidyUpAsync(CancellationToken cancellationToken);
    }
}
