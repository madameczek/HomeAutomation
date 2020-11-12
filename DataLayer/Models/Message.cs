using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string MessageBody { get; set; }
        [Required]
        public bool IsProcessed { get; set; }

        // Relationships
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }
    }
}
