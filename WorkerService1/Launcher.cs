using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    class Launcher : IHostedService, IDisposable
    {
        private Timer _timer1;
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        private readonly ILogger _logger;
        private readonly Service1BusinessLayer _service;
        private IServiceProvider Services { get; }
        public Launcher(ILoggerFactory logger, IServiceProvider services, Service1BusinessLayer businessService)
        {
            _logger = logger.CreateLogger("Usługa");
            _service = businessService;
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _timer1 = new Timer(JobOne, null, TimeSpan.FromMilliseconds(100),
                TimeSpan.FromSeconds(5));
                _logger.LogDebug("Starting");
                //Task.Delay(2000).Wait(cancellationToken);
                return Task.CompletedTask;
            }
            catch (OperationCanceledException) // tu nie wpadnie, bo nie ma co wywołać exc, no ale  powinno byc
            {
                _logger.LogInformation("Cancelled at StartAsync");
                return Task.FromCanceled(cancellationToken);
            }
        } 

        private void JobOne(object state)
        {
            _executingTask = _service.DoWorkAsync(_stoppingCts.Token);
            _logger.LogDebug("JobOne started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer1?.Change(Timeout.Infinite, 0);
            try
            {
                _stoppingCts.Cancel();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Cancelled at Launcher StopAsync");
            }
            finally
            {
                _logger.LogInformation("Application is stopping");
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing resources.");
            _timer1.Dispose();
        }
    }
}
