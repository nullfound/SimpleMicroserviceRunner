using SimpleMicroserviceRunner.Runtime.Builder;
using SimpleMicroserviceRunner.Runtime.Config;
using SimpleMicroserviceRunner.Runtime.Runner;

namespace SimpleMicroserviceRunner.Sample.Basic
{
    public static class Program
    {
        public static void Main()
        {
            MicroserviceBuilder.WithMicroservice<HelloWorldMicroservice>()
                .WithRunner<ConsoleMicroserviceRunner>()
                .Run();
        }
    }
}
