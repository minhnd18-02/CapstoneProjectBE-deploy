using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BoardDTO
{
    public class CreateBoardDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;
        public string? Label { get; set; }
        [Required(ErrorMessage = "Project is required")]
        public int ProjectId { get; set; }
        public bool Status { get; set; } = false;
    }
}
