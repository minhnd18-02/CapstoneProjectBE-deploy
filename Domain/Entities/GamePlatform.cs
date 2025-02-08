using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GamePlatform
    {
        public int PlatformId { get; set; }
        public int GameId { get; set; }

        // Relationships
        public virtual Game Game { get; set; } = null!;
        public virtual Platform Platform { get; set; } = null!;
    }
}
