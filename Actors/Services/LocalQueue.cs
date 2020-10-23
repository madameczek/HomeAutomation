using Actors.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Actors.Services
{
    public class LocalQueue
    {
        private LocalContext context;

        public LocalQueue(LocalContext context)
        {
            this.context = context;
        }

        public async Task Run(CancellationToken ct = default)
        {
            // uruchamia metody ReadData() z serwisów i pcha do bazy
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    Console.WriteLine(this.context.Database.ProviderName);

                    await Task.Delay(3000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
        }
    }
}
