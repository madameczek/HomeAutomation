using System;
using System.Collections.Generic;
using System.Text;

namespace CommonClasses.Interfaces
{
    public interface IMessage
    {
        public int? Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string MessageBody { get; set; }
        public bool? IsProcessed { get; set; }
        public Guid ActorId { get; set; }
    }
}
