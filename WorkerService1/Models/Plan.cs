using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkerService1.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid? UserId { get; set; }
    }
}
