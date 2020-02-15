using System.Threading;

namespace SimpleMicroserviceRunner.Runtime.Host
{
    public enum HostStatusEnum
    {
        /// <summary>
        /// Default
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Unexpected error
        /// </summary>
        Error,

        /// <summary>
        /// Microservice is starting
        /// </summary>
        MicroserviceStarting,

        /// <summary>
        /// Microservice has started
        /// </summary>
        MicroserviceStarted,

        /// <summary>
        /// Microservice has stopped
        /// </summary>
        MicroserviceStopped,

        /// <summary>
        /// Host is initialising
        /// </summary>
        HostInitialising,

        /// <summary>
        /// Host has initialised
        /// </summary>
        HostInitialised,

        /// <summary>
        /// Host has started
        /// </summary>
        HostStarted,

        /// <summary>
        /// Host is ready for shutdown
        /// </summary>
        HostShutdown,
    }
}
