using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleMicroserviceRunner.Runtime.Config;

namespace SimpleMicroserviceRunner.Runtime.Host
{
    internal class MicroserviceHost : IMicroserviceHost
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly MicroserviceConfig config;
        private Task microserviceRunTask;

        public MicroserviceHost(MicroserviceConfig config)
        {
            this.config = config;
        }

        private HostStatusEnum Status { get; set; }

        public async Task StartAsync(CancellationToken token)
        {
            this.UpdateStatus(HostStatusEnum.HostStarted);

            try
            {
                this.UpdateStatus(HostStatusEnum.HostInitialising);

                this.Initialise(token);

                this.UpdateStatus(HostStatusEnum.HostInitialised);

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
                await this.microserviceRunTask.ConfigureAwait(false);

#pragma warning disable CS4014
                Task.Factory.StartNew(
                       () => this.WaitForCancellationAndShutdownAsync().Wait(),
                       CancellationToken.None,
                       TaskCreationOptions.LongRunning,
                       TaskScheduler.Current);
#pragma warning restore CS4014
            }
            catch (OperationCanceledException)
            {
                this.UpdateStatus(HostStatusEnum.HostShutdown);
            }
            catch (Exception)
            {
                this.UpdateStatus(HostStatusEnum.Error);
                throw;
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
            Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
            {
                args.Cancel = true;
                this.cancellationTokenSource.Cancel();
            });

            token.Register(() => this.cancellationTokenSource.Cancel());
            this.cancellationTokenSource.Token.ThrowIfCancellationRequested();

            Console.WriteLine("Press ctrl-c to stop.");
        }

        internal virtual async Task WaitForCancellationAndShutdownAsync()
        {
            try
            {
                await Task.WhenAll(this.microserviceRunTask).ConfigureAwait(false);
                this.UpdateStatus(HostStatusEnum.MicroserviceStopped);
            }
            catch (Exception)
            {
                // log error
                this.UpdateStatus(HostStatusEnum.Error);
            }
            finally
            {
                this.UpdateStatus(HostStatusEnum.HostShutdown);
                this.cancellationTokenSource.Cancel();
            }
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
