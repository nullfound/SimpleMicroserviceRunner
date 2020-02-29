using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Plugin.DependencyInjection;
using SimpleMicroserviceRunner.Runtime;

namespace SimpleMicroserviceRunner.Sample.BasicWithDI
{
    public class BasicDIMicroservice : IMicroservice
    {
        private readonly Lazy<IRandomNumberCreator> randomNumberCreatorFactory;

        public BasicDIMicroservice()
        {
            this.randomNumberCreatorFactory = new Lazy<IRandomNumberCreator>(() => ContainerWrapper.Container.GetInstance<IRandomNumberCreator>());
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                await Task.Delay(500).ConfigureAwait(false);

                Console.WriteLine($"Random Number: {this.randomNumberCreatorFactory.Value.CreateRandomNumner()}.");

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
