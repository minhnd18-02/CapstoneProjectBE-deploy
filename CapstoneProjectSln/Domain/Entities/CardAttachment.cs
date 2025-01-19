using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CardAttachment
    {
        public int CardId { get; set; }
        public int FileId { get; set; }
        public virtual Card Card { get; set; } = null!;
        public virtual File File { get; set; } = null!;

    }
}
