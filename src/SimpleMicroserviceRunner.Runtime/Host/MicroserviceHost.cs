using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Runtime.Config;
using SimpleMicroserviceRunner.Runtime.Plugin;

namespace SimpleMicroserviceRunner.Runtime.Host
{
    internal class MicroserviceHost : IMicroserviceHost
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly MicroserviceConfig config;
        private readonly IPluginRunner pluginRunner;
        private Task microserviceRunTask;

        public MicroserviceHost(MicroserviceConfig config)
        {
            this.config = config;
            this.pluginRunner = this.config.PluginRunnerFactory.Invoke();
        }

        private HostStatusEnum Status { get; set; }

        public async Task StartAsync(CancellationToken token)
        {
            this.UpdateStatus(HostStatusEnum.HostStarted);

            try
            {
                this.Initialise(token);

                await this.pluginRunner.RunPluginsAsync(PluginState.OnHostInitialisation, this.cancellationTokenSource.Token).ConfigureAwait(false);

                await this.pluginRunner.RunPluginsAsync(PluginState.BeforeMicroserviceStart, this.cancellationTokenSource.Token).ConfigureAwait(false);

                this.StartMicroservice();

                await this.WaitForMicroserviceToStopAsync().ConfigureAwait(false);

                await this.pluginRunner.RunPluginsAsync(PluginState.AfterMicroserviceShutdown, this.cancellationTokenSource.Token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                this.UpdateStatus(HostStatusEnum.Error);
                throw;
            }
            finally
            {
                await this.pluginRunner.RunPluginsAsync(PluginState.BeforeHostShutdown, this.cancellationTokenSource.Token).ConfigureAwait(false);

                this.Shutdown();

                await this.pluginRunner.RunPluginsAsync(PluginState.TidyUp, this.cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal virtual void UpdateStatus(HostStatusEnum status)
        {
            Console.WriteLine($"Status transition: {this.Status} --> {status}.");
            this.Status = status;
        }

        internal virtual void Initialise(CancellationToken token)
        {
            this.UpdateStatus(HostStatusEnum.HostInitialising);

            Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
            {
                args.Cancel = true;
                this.cancellationTokenSource.Cancel();
            });

            token.Register(() => this.cancellationTokenSource.Cancel());
            this.cancellationTokenSource.Token.ThrowIfCancellationRequested();

            Console.WriteLine("Press ctrl-c to stop.");

            this.UpdateStatus(HostStatusEnum.HostInitialised);
        }

        internal virtual void StartMicroservice()
        {
            this.UpdateStatus(HostStatusEnum.MicroserviceStarting);

            this.microserviceRunTask = Task.Factory.StartNew(
                () =>
                {
                    this.UpdateStatus(HostStatusEnum.MicroserviceStarted);
                    this.config.Microservice.RunAsync(this.cancellationTokenSource.Token).Wait();
                },
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

        internal virtual async Task WaitForMicroserviceToStopAsync()
        {
            // if the microservice is a blocking call then the host will wait here until the cancellation is manually triggered.
            await Task.WhenAll(this.microserviceRunTask).ConfigureAwait(false);
            this.UpdateStatus(HostStatusEnum.MicroserviceStopped);
        }

        internal virtual void Shutdown()
        {
            // if there are any errors in the host. we want to be able to send a cancellation to the microservice
            this.UpdateStatus(HostStatusEnum.HostShutdown);
            this.cancellationTokenSource.Cancel();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.cancellationTokenSource?.Dispose();
            }
        }
    }
}
