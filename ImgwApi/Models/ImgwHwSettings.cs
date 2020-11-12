using Shared;
using Shared.Models;
using System;

namespace ImgwApi.Models
{
    public class ImgwHwSettings : IImgwHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public DeviceType Type { get; set; }
        public string Name { get; set; }
        public bool Attach { get; set; }
        public string Interface { get; set; }

        /// <summary>
        /// Interval between subsequent readings of IMGW API in minutes.
        /// If can't be read from config file, default value is applied.
        /// Default value is set in service's launcher object for 20 minutes.
        /// </summary>
        public int ReadInterval { get; set; }
        public string Url { get; set; }
        public int StationId { get; set; }
    }
}
