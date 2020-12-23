using Shared.Models;
using System;

namespace Relay.Models
{
    internal class SunriseSunsetApiData : IMessage
    {
        public int? Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string MessageBody { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }

        public int DayLengthSeconds { get; set; }
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
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
