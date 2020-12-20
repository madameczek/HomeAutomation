using System;

namespace Shared.Models
{
    public interface IHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        //public DeviceType Type { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// True if device should be connected to a service.
        /// If false, service isn't initialised.
        /// </summary>
        public bool Attach { get; set; }
        public string Interface { get; set; }

        /// <summary>
        /// Interval between subsequent readings of a sensor.
        /// If can't be read from config file, default value is applied.
        /// Default value is set in service's launcher object.
        /// Units may be millicesonds, seconds or minutes.
        /// </summary>
        public int ReadInterval { get; set; }
    }
}
