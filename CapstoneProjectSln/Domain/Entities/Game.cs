using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime PublishDatetime { get; set; }
        public string? Details { get; set; }
        public decimal Price { get; set; }

        // Relationships
        public virtual ICollection<GameCategory> GameCategories { get; set; } = new List<GameCategory>();
        public virtual ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
    }
}
