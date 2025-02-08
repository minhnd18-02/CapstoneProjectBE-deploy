using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProjectDTO
{
    public class CreateProjectDto
    {
        public int TeamId { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }
    }
}
