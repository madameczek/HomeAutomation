
using CommonClasses.Interfaces;
using System;

namespace ImgwApi.Models
{
    public class WeatherMessage : IMessage
    {
        public int? Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string MessageBodyJson { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }
    }
}
