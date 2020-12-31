using System;
using System.Collections.Generic;
using System.Text;
using Shared.Models;

namespace Relay.Models
{
    public class RelayHwSettings : IHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public bool Attach { get; set; }
        public string Interface { get; set; }

        /// <summary>
        /// Interval between subsequent readings in seconds.
        /// </summary>
        public int ReadInterval { get; set; }
        public int GpioPin { get; set; }
        public int ActiveState { get; set; }
        public string ActivateOn { get; set; }
        public string DeactivateOn { get; set; }
        public TimeSpan TimeOn { get; set; }
        public TimeSpan TimeOff { get; set; }
        public TimeSpan Timer { get; set; }
    }
}
