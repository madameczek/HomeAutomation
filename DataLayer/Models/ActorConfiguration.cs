using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class ActorConfiguration
    {
        private DateTime _updatedOn;

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime UpdatedOn { get => _updatedOn.ToLocalTime(); set => _updatedOn = value.ToUniversalTime(); }
        public string ConfigurationJson { get; set; }

        // Relationships
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }
    }
}
