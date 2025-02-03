using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TeamMemberDTO
{
    public class TeamMemberDTO
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }
    }
}

