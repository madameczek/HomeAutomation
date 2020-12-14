using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Weather
    {
        private DateTime _createdOn;

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get => _createdOn.ToLocalTime(); set => _createdOn = value.ToUniversalTime(); }
        [Required]
        public bool IsProcessed { get; set; }
        public double? AirTemperature { get; set; }
        public double? AirPressure { get; set; }
        public double? Precipitation { get; set; }
        public double? Humidity { get; set; }
        public double? WindSpeed { get; set; }
        public int? WindDirection { get; set; }
        public int? StationId { get; set; }
        public string StationName { get; set; }
    }
}
