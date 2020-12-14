using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Message
    {
        private DateTime _createdOn;

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get => _createdOn.ToLocalTime(); set => _createdOn = value.ToUniversalTime(); }
        public string MessageBody { get; set; }
        [Required]
        public bool IsProcessed { get; set; }

        // Relationships
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }
    }
}
