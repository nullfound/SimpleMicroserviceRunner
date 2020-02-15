using SimpleMicroserviceRunner.Runtime.Config;

namespace SimpleMicroserviceRunner.Runtime.Builder
{
    public static class MicroserviceBuilder
    {
        public static MicroserviceConfig WithMicroservice<T>()
            where T : IMicroservice, new()
        {
            var simpleMicroserviceConfig = new MicroserviceConfig();
            simpleMicroserviceConfig.Microservice = new T();
            return simpleMicroserviceConfig;
        }
    }
}
