using CommonClasses;
using CommonClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImgwApi.Models
{
    public class HwSettings : IHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public DeviceType Type { get; set; }
        public string Name { get; set; }
        public bool Attach { get; set; }
        public string Interface { get; set; }
        private int _readInterval;
        /// <summary>
        /// Return milliseconds from minutes stored in configuration file.
        /// </summary>
        public int ReadInterval { get => _readInterval; set => _readInterval = value * 60000 > int.MaxValue ? int.MaxValue : value * 60000; }
        public string Url { get; set; }
        public int StationId { get; set; }
    }
}
