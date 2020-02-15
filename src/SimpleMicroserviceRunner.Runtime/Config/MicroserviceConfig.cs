namespace SimpleMicroserviceRunner.Runtime.Config
{
    public class MicroserviceConfig
    {
        public IMicroservice Microservice { get; internal set; }

        public IMicroserviceRunner Runner { get; internal set; }
    }
}
