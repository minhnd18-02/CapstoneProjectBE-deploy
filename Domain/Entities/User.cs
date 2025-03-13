﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        //public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDatetime { get; set; }

        // Relationships
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<File> Files { get; set; } = new List<File>();
        public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
        public virtual ICollection<Pledge> Pledges { get; set; } = new List<Pledge>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
