using Actors.Models.LocalDbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Actors.Models.LocalDbModels
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset CreatedOn { get; set; }
        public string MessageBodyJson { get; set; }
        [Required]
        public bool IsProcessed { get; set; }

        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
    }
}
