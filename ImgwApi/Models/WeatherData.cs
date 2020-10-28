﻿using CommonClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImgwApi.Models
{
    class WeatherData : IMessage
    {
        public int? Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string MessageBodyJson { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }

        public double AirTemperature { get; set; }
        public double AirPressure { get; set; }
        public double Precipitation { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
    }
}