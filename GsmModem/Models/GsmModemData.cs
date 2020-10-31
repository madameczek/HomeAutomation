using CommonClasses.Interfaces;
using System;

namespace GsmModem.Models
{
    public class GsmModemData : IMessage
    {
        public int? Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string MessageBody { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
    }
}
