using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Runtime.Host;

namespace SimpleMicroserviceRunner.Runtime.Runner
{
    public class ConsoleMicroserviceRunner : IMicroserviceRunner
    {
        public void Run(IMicroserviceHost host, CancellationToken? token = null)
        {
            Task.Factory.StartNew(
                () =>
                {
                    host.StartAsync(token ?? CancellationToken.None).Wait();
                },
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current)
                .Wait();
        }

        public void OnShutdown()
        {
            // Dispose objects
        }
    }
}
