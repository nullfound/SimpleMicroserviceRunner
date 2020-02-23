using System;
using System.Collections.Generic;
using SimpleMicroserviceRunner.Runtime.Plugin;

namespace SimpleMicroserviceRunner.Runtime.Config
{
    public class MicroserviceConfig
    {
        public IMicroservice Microservice { get; internal set; }

        public IMicroserviceRunner Runner { get; internal set; }

        public List<IPlugin> Plugins { get; } = new List<IPlugin>();

        internal Func<IPluginRunner> PluginRunnerFactory => () => new PluginRunner(this.Plugins);
    }
}
