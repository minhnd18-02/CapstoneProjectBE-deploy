using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class File
    {
        public int FileId { get; set; }
        public bool Status { get; set; }
        public string Source { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public virtual ICollection<PostAttachment> PostAttachments { get; set; } = new List<PostAttachment>();
        public virtual ICollection<CardAttachment> CardAttachments { get; set; } = new List<CardAttachment>();

    }
}
