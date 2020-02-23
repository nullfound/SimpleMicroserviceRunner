using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Utils;

namespace SimpleMicroserviceRunner.Runtime.Plugin
{
    public class PluginRunner : IPluginRunner
    {
        private readonly List<IPlugin> plugins;

        public PluginRunner(List<IPlugin> plugins)
        {
            this.plugins = plugins;
        }

        public async Task RunPluginsAsync(PluginState state, CancellationToken token)
        {
            var priorityGroupedPlugins = this.plugins.GroupBy(_ => _.PluginExecutionPriority).OrderByDescending(c => c.Key);

            try
            {
                foreach (var priorityGroup in priorityGroupedPlugins)
                {
                    switch (state)
                    {
                        case PluginState.OnHostInitialisation:
                            await priorityGroup.ParallelForEachAsync(plugin => plugin.RunOnHostInitialisationAsync(token)).ConfigureAwait(false);
                            break;
                        case PluginState.BeforeMicroserviceStart:
                            await priorityGroup.ParallelForEachAsync(plugin => plugin.RunBeforeMicroserviceStartAsync(token)).ConfigureAwait(false);
                            break;
                        case PluginState.AfterMicroserviceShutdown:
                            await priorityGroup.ParallelForEachAsync(plugin => plugin.RunAfterMicroserviceShutdownAsync(token)).ConfigureAwait(false);
                            break;
                        case PluginState.BeforeHostShutdown:
                            await priorityGroup.ParallelForEachAsync(plugin => plugin.RunBeforeHostShutdownAsync(token)).ConfigureAwait(false);
                            break;
                        case PluginState.TidyUp:
                            await priorityGroup.ParallelForEachAsync(plugin => plugin.RunTidyUpAsync(token)).ConfigureAwait(false);
                            break;
                        default:
                            throw new NotImplementedException($"Plugin state: '{state}' is not implemented.");
                    }
                }
            }
            catch (Exception)
            {
                // log error.
                throw;
            }
        }
    }
}
