using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerService1.Models;

namespace WorkerService1
{
    internal interface IScopedDataAccessLayer1
    {
          Task<List<Plan>> GetPlans(CancellationToken cancellationToken = default);
    }
}
