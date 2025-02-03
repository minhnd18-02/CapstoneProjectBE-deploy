using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Assignation
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public virtual Card Card { get; set; } = null!;
        public virtual User User { get; set; } = null!;

    }
}
