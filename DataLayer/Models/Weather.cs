﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Weather
    {
        private DateTime _createdOn;

        [Key]
        public int Id { get; set; }
        
        // In database dates are stored as UTC.
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
        [StringLength(100)]
        public string StationName { get; set; }
    }
}
