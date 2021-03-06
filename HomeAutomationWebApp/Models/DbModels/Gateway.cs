﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeAutomationWebApp.Models.DbModels
{
    public class Gateway
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SiteName { get; set; }

        // Relationships
        public ICollection<Actor> Actors { get; set; }
        [ForeignKey("IotUser")]
        public Guid IotUserId { get; set; }
    }
}
