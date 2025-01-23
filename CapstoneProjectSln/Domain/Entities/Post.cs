using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string? Details { get; set; }
        public DateTime CreatedDatetime { get; set; }

        // Relationships
        public virtual User User { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<PostAttachment> PostAttachments { get; set; } = new List<PostAttachment>();

    }
}
