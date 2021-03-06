﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Text;

namespace HomeAutomationWebApp.Models.DbModels
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
        public ICollection<TemperatureAndHumidity> Temperatures { get; set; }
        public ICollection<QueueItem> QueueItems { get; set; }
        [ForeignKey("Gateway")]
        public Guid GatewayId { get; set; }
    }
}
