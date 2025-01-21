using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime CreatedDatetime { get; set; }

        // Relationships
        public ICollection<Token> Tokens { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}
