using Actors.Models.LocalDbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Text;

namespace Actors.Models.LocalDbModels
{
    public class Actor
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Type { get; set; }
        public ActorConfiguration Configuration { get; set; }

        // Relationships
        public ICollection<Message> Messages { get; set; }
        [ForeignKey("Gateway")]
        public Guid GatewayId { get; set; }
    }
}
