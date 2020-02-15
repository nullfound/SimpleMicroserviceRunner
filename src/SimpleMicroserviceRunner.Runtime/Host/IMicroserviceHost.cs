using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMicroserviceRunner.Runtime.Host
{
    public interface IMicroserviceHost : IDisposable
    {
        Task StartAsync(CancellationToken token);
    }
}
