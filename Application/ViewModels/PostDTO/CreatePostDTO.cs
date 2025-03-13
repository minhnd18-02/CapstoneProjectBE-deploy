﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PostDTO
{
    public class CreatePostDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can't be longer than 50 characters")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty;

    }
}
