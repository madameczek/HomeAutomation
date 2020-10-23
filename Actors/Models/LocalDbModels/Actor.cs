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
        [Required]
        public ActorConfiguration Configuration { get; set; }

        [ForeignKey("GatewayGuid")]
        public Gateway Gateway { get; set; }
    }
}
