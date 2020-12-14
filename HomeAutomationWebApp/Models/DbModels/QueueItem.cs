using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Models.DbModels
{
    public class QueueItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string MessageBody { get; set; }
        [Required]
        public bool IsProcessed { get; set; }
        [Required]
        public int Direction { get; set; } // out = 0, in = 1

        // Relationships
        [ForeignKey("Actor")]
        public Guid? ActorId { get; set; }
    }
}
