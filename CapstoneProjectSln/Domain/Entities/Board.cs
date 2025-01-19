using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Board
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        // Relationships
    }
}
