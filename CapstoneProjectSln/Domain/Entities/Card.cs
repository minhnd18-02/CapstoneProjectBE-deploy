using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public virtual ICollection<Assignation> Assignations { get; set; } = new List<Assignation>();
        public virtual ICollection<CardAttachment> CardAttachments { get; set; } = new List<CardAttachment>();
        // Relationships
    }
}
