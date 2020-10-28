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
        public int ReadInterval { get; set; }
        public string Url { get; set; }
        public int StationId { get; set; }
    }
}
