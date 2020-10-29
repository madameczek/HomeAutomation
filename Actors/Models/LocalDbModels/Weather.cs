using System;
using System.ComponentModel.DataAnnotations;

namespace Actors.Models.LocalDbModels
{
    public class Weather
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
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
