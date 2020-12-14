using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IotHubGatewayDaemon.Models
{
    public class Gateway
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SiteName { get; set; }

        // Relationships
        public ICollection<Actor> Actors { get; set; }
    }
}
