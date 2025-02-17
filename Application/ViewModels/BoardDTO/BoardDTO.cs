using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BoardDTO
{
    public class BoardDTO
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDatetime { get; set; } = DateTime.MinValue;

    }
}
