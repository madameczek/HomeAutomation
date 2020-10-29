using CommonClasses.Interfaces;
using System;

namespace TemperatureSensor.Models
{
    public class TemperatureSensorMessage : IMessage
    {
        public int? Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string MessageBody { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }
    }
}
