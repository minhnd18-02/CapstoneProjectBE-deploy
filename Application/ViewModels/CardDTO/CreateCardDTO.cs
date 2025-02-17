using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CardDTO
{
    public class CreateCardDTO
    {
        [Required(ErrorMessage = "Board is required")]
        public int BoardId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;
        [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
        public string? Description { get; set; }
        public bool Status { get; set; } = false;
        public DateTime? Deadline { get; set; }
    }
}
