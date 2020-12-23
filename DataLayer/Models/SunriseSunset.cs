using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class SunriseSunset
    {
        private DateTime _createdOn;
        private DateTime _sunrise;
        private DateTime _sunset;
        private DateTime _solarNoon;
        private DateTime _civilTwilightBegin;
        private DateTime _civilTwilightEnd;
        private DateTime _nauticalTwilightBegin;
        private DateTime _nauticalTwilightEnd;
        private DateTime _astronomicalTwilightBegin;
        private DateTime _astronomicalTwilightEnd;

        [Key]
        public int Id { get; set; }

        // In database dates are stored as UTC.
        [Required]
        public DateTime CreatedOn { get => _createdOn.ToLocalTime(); set => _createdOn = value.ToUniversalTime(); }
        [Required]
        public bool IsProcessed { get; set; }

        [Required]
        public DateTime Sunrise { get => _sunrise.ToLocalTime(); set => _sunrise = value.ToUniversalTime(); }
        [Required]
        public DateTime Sunset { get => _sunset.ToLocalTime(); set => _sunset = value.ToUniversalTime(); }
        [Required]
        public DateTime SolarNoon { get => _solarNoon.ToLocalTime(); set => _solarNoon = value.ToUniversalTime(); }
        [Required]
        public TimeSpan DayLength { get; set; }
        [Required]
        public DateTime CivilTwilightBegin { get => _civilTwilightBegin.ToLocalTime(); set => _civilTwilightBegin = value.ToUniversalTime(); }
        [Required]
        public DateTime CivilTwilightEnd { get => _civilTwilightEnd.ToLocalTime(); set => _civilTwilightEnd = value.ToUniversalTime(); }
        [Required]
        public DateTime NauticalTwilightBegin { get => _nauticalTwilightBegin.ToLocalTime(); set => _nauticalTwilightBegin = value.ToUniversalTime(); }
        [Required]
        public DateTime NauticalTwilightEnd { get => _nauticalTwilightEnd.ToLocalTime(); set => _nauticalTwilightEnd = value.ToUniversalTime(); }
        [Required]
        public DateTime AstronomicalTwilightBegin { get => _astronomicalTwilightBegin.ToLocalTime(); set => _astronomicalTwilightBegin = value.ToUniversalTime(); }
        [Required]
        public DateTime AstronomicalTwilightEnd { get => _astronomicalTwilightEnd.ToLocalTime(); set => _astronomicalTwilightEnd = value.ToUniversalTime(); }
        [Required]
        [StringLength(100)]
        public string Location { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
