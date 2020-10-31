using HomeAutomationWebApp.Data;
using HomeAutomationWebApp.Models.DbModels;
using HomeAutomationWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Services
{
    public class DashboardService : IDashboardService
    {
        public IList<Weather> GetAll()
        {
            var _result = _context.WeatherReadings.ToList();
            return _result;
        }

        public void PhoneRing()
        {
            var _message = new Dictionary<string, string>();
            var _queueItem = new QueueItem();

            _context.Queue.Add(new QueueItem());
        }

        #region Dependency Injection
        private readonly AzureDbContext _context;
        public DashboardService(AzureDbContext context)
        {
            _context = context;
        }
        #endregion
    }
}
