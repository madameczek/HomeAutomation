using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class QueueItemLocal
    {
        private DateTime _createdOn;

        [Key]
        public int Id { get; set; }

        // In database dates are stored as UTC.
        [Required]
        public DateTime CreatedOn { get => _createdOn.ToLocalTime(); set => _createdOn = value.ToUniversalTime(); }
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
