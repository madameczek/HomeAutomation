using Shared.Models;
using System;

namespace TemperatureSensor.Models
{
    public class TemperatureSensorData : IMessage
    {
        public int? Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string MessageBody { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
    }
}
