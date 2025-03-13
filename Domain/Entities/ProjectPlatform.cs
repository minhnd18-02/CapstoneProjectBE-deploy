using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectPlatform
    {
        public int PlatformId { get; set; }
        public int ProjectId { get; set; }

        // Relationships
        public virtual Project Project { get; set; } = null!;
        public virtual Platform Platform { get; set; } = null!;
    }
}
