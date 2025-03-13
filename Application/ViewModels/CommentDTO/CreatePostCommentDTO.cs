﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CommentDTO
{
    public class CreatePostCommentDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public int? ParentCommentId { get; set; }
        [Required(ErrorMessage = "Post ID is required")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [StringLength(255, ErrorMessage = "Content can't be longer than 50 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
