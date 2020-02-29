using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Plugin;

namespace SimpleMicroserviceRunner.Runtime.Plugin
{
    public interface IPluginRunner
    {
        Task RunPluginsAsync(PluginState state, CancellationToken token);
    }
}
