using CommonClasses.Interfaces;
using System;

namespace TemperatureSensor.Models
{
    class TemperatureSensorData : IMessage
    {
        public int? Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string MessageBodyJson { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }

        public double? Temperature { get; set; }

    }
}
