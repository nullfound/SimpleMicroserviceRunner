using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Runtime;

namespace SimpleMicroserviceRunner.Sample.Basic
{
    public class HelloWorldMicroservice : IMicroservice
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                await Task.Delay(500).ConfigureAwait(false);

                Console.WriteLine("Hello World!");

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
