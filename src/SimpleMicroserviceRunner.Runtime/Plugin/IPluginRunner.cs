using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMicroserviceRunner.Runtime.Plugin
{
    public interface IPluginRunner
    {
        Task RunPluginsAsync(PluginState state, CancellationToken token);
    }
}
