using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IotHubGateway.Models
{
    public class TemperatureAndHumidity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset CreatedOn { get; set; }
        [Required]
        public bool IsProcessed { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }

        // Relationships
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }

        public TemperatureAndHumidity Clone()
        {
            return (TemperatureAndHumidity)MemberwiseClone();
        }
    }
}