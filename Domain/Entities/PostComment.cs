using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostComment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; } 
        // Relationships
        public virtual Post Post { get; set; } = null!;
        public virtual Comment Comment { get; set; } = null!;
    }
}
