using System.Threading;
using SimpleMicroserviceRunner.Runtime.Host;

namespace SimpleMicroserviceRunner.Runtime
{
    public interface IMicroserviceRunner
    {
        void Run(IMicroserviceHost host, CancellationToken? token = null);

        void OnShutdown();
    }
}
