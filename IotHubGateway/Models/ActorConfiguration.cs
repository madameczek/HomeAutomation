using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IotHubGateway.Models
{
    public class ActorConfiguration
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset UpdatedOn { get; set; }
        public string ConfigurationJson { get; set; }

        // Relationships
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }
    }
}
