using SimpleMicroserviceRunner.Plugin.DependencyInjection;
using SimpleMicroserviceRunner.Runtime.Builder;
using SimpleMicroserviceRunner.Runtime.Config;
using SimpleMicroserviceRunner.Runtime.Runner;

namespace SimpleMicroserviceRunner.Sample.BasicWithDI
{
    public static class Program
    {
        public static void Main()
        {
            MicroserviceBuilder.WithMicroservice<BasicDIMicroservice>()
                .WithRunner<ConsoleMicroserviceRunner>()
                .WithPlugin<SimpleInjectorPlugin>()
                .Run();
        }
    }
}
