using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Relay.Models
{
    internal class SunriseSunsetApiData
    {
        public int DayLengthSeconds { get;  set; }

        public DateTime Sunrise { get; set; }

        public DateTime Sunset { get; set; }

        public DateTime SolarNoon { get; set; }

        public TimeSpan DayLength => TimeSpan.FromSeconds(DayLengthSeconds);

        public DateTime CivilTwilightBegin { get; set; }
        public DateTime CivilTwilightEnd { get; set; }
        public DateTime NauticalTwilightBegin { get; set; }
        public DateTime NauticalTwilightEnd { get; set; }
        public DateTime AstronomicalTwilightBegin { get; set; }
        public DateTime AstronomicalTwilightEnd { get; set; }
    }
}
