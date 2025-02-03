using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CardDTO
{
    public class CardDTO
    {
        public int CardId { get; set; }
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}
