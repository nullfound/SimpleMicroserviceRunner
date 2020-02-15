using System.Threading;
using System.Threading.Tasks;

namespace SimpleMicroserviceRunner.Runtime
{
    public interface IMicroservice
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
