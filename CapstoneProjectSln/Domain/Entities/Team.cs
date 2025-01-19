using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relationships
        public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}
