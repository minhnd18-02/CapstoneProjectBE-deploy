using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TeamMember
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }

        // Relationships
        public virtual Team Team { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
