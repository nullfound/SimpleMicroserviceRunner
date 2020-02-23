namespace SimpleMicroserviceRunner.Runtime.Plugin
{
    public enum PluginState
    {
        /// <summary>
        /// Unknown state
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Executes plugins on host initialisation
        /// </summary>
        OnHostInitialisation = 1,

        /// <summary>
        /// Executes plugins before microservice started
        /// </summary>
        BeforeMicroserviceStart = 2,

        /// <summary>
        /// Executes plugins after microservice shutdown
        /// </summary>
        AfterMicroserviceShutdown = 3,

        /// <summary>
        /// Executes plugins before host shutdown
        /// </summary>
        BeforeHostShutdown = 4,

        /// <summary>
        /// Executes plugins for tidy up
        /// </summary>
        TidyUp = 5,
    }

    public enum PluginExecutionPriorityEnum
    {
        /// <summary>
        /// Lowest execution priority.
        /// </summary>
        Lowest = 0,

        /// <summary>
        /// Between Low and Lowest execution priority.
        /// </summary>
        VeryLow = 1,

        /// <summary>
        /// Low execution priority.
        /// </summary>
        Low = 2,

        /// <summary>
        /// Normal execution priority.
        /// </summary>
        Normal = 3,

        /// <summary>
        /// Between High and Normal execution priority.
        /// </summary>
        AboveNormal = 4,

        /// <summary>
        /// High execution priority.
        /// </summary>
        High = 5,

        /// <summary>
        /// Between Highest and High execution priority.
        /// </summary>
        VeryHigh = 6,

        /// <summary>
        /// Highest execution priority.
        /// </summary>
        Highest = 7,
    }
}
