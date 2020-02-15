using System;
using System.Threading;
using SimpleMicroserviceRunner.Runtime.Host;

namespace SimpleMicroserviceRunner.Runtime.Config
{
    public static class MicroserviceConfigExtensions
    {
        public static MicroserviceConfig WithRunner<T>(this MicroserviceConfig setup)
            where T : IMicroserviceRunner, new()
        {
            setup.Runner = new T();
            return setup;
        }

        public static void Run(this MicroserviceConfig config, CancellationToken? token = null)
        {
            if (config.Runner == null)
            {
                throw new ArgumentNullException(nameof(config), "Runner for a microservice cannot be null.");
            }

            try
            {
                // A System.ServiceProcess.ServiceBase runner might require more things to be initialised.
                config.Runner.OnStart();

                using (var host = new MicroserviceHost(config))
                {
                    config.Runner.Run(host, token);
                }
            }
            catch (Exception)
            {
                // Todo need a logger
                throw;
            }
            finally
            {
                // Allowing each runner to be able to perform any required clean up.
                config.Runner.OnShutdown();
            }
        }
    }
}
