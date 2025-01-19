using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameCategory
    {
        public int CategoryId { get; set; }
        public int GameId { get; set; }

        // Relationships
        public virtual Game Game { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
    }
}
