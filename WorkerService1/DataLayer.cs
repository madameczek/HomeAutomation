using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerService1.Models;

namespace WorkerService1
{
    internal class DataLayer : IScopedDataAccessLayer1
    {
        private readonly ILogger _logger;
        private readonly Context _context;


        public DataLayer(ILoggerFactory loggerFactory, Context context)
        {
            _logger = loggerFactory.CreateLogger("Data Layer");
            _context = context;
            _logger.LogDebug("Data Access Layer: {Guid} created with context: {Context}", this.GetHashCode(), context.ContextId.ToString()[^6..^2]);
        }

        public Task<List<Plan>> GetPlans(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("GetPlans invoked");

                //var result = _context.Plans.ToListAsync(cancellationToken);
                var result = Task.FromResult<List<Plan>>(new List<Plan>() { new Plan() { Description = "mocked opis" } }); // mocking empty result from database
                result.Wait(cancellationToken);
                
                _logger.LogDebug("Cancellation token canBeCancelled: {token}", cancellationToken.CanBeCanceled);

                return result;
            }
            catch (OperationCanceledException) 
            {
                _logger.LogInformation("Cancelled at GetPlans");
                return (Task<List<Plan>>)Task.FromCanceled(cancellationToken);
            }
        }
    }
}
