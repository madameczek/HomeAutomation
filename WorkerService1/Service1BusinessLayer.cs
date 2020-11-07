using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    class Service1BusinessLayer
    {
        private readonly ILogger _logger;
        //private readonly IScopedDataAccessLayer1 _dataAccess;
        private IServiceProvider Services { get; }

        public Service1BusinessLayer(ILoggerFactory logger, IServiceProvider services)
        {
            _logger = logger.CreateLogger("Service 1 Business Layer");
            Services = services;
        }

        internal async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using (var scope = Services.CreateScope())
                {
                    _logger.LogDebug("DoWork at Service 1 Business Layer invoked. Id: {BusinessLayer}", this.GetHashCode());
                    var scopedService = scope.ServiceProvider.GetRequiredService<IScopedDataAccessLayer1>();
                    var plans = await scopedService.GetPlans(cancellationToken);
                    //plans.Wait();
                    plans.ForEach(p => Console.WriteLine($"Plan: {p.Description}"));
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Cancelled at DoWork async");
            }
        }
    }
}
