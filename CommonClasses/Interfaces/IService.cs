using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonClasses.Interfaces
{
    public interface IService
    {
        public string HwSettingsSection { get; }
        public string HwSettingsActorSection { get; }

        /// <summary>
        /// 
        /// </summary>
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Device Type
        /// </summary>
        public Enum Type { get; set; }

        /// <summary>
        /// Serialised information about device configuration. Stored as string in a database
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Information about device configuration in dictionary form - easier to work with.
        /// </summary>
        public IDictionary<string, string> ConfigurationJson { get; set; }

        public Task ConfigureService(CancellationToken cancellationToken);

        public IService ReadConfig();

        public IMessage GetMessage();

        public bool Write(IMessage message);

        /// <summary>
        /// Method to be invoked in order to start and run a service. Defines tasks, which are invoked periodically.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Run(CancellationToken cancellationToken);
    }
}
