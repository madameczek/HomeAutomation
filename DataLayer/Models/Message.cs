using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Message
    {
        private DateTime _createdOn;

        [Key]
        public int Id { get; set; }

        // In database dates are stored as UTC.
        [Required]
        public DateTime CreatedOn
        {
            get => _createdOn.ToLocalTime(); 
            set => _createdOn = value.ToUniversalTime();
        }
        public string MessageBody { get; set; }
        [Required]
        public bool IsProcessed { get; set; }
        public int? Type { get; set; }

        // Relationships
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }
    }
}
