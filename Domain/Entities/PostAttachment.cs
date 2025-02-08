using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostAttachment
    {
        public int PostId { get; set; }
        public int FileId { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual File File { get; set; } = null!;

    }
}
