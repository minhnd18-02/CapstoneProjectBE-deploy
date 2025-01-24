using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        { }

        public DbSet<Assignation> Assignations { get; set; }
        public DbSet<PaymentLinkInformation> PaymentLinkInformation { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CardAttachment> CardAttachments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here if needed
            modelBuilder.Entity<Assignation>()
                .HasKey(a => new { a.CardId, a.UserId });

            modelBuilder.Entity<TeamMember>()
                .HasKey(tm => new { tm.TeamId, tm.UserId });

            modelBuilder.Entity<GamePlatform>()
                .HasKey(gp => new { gp.GameId, gp.PlatformId });

            modelBuilder.Entity<CardAttachment>()
                .HasKey(gp => new { gp.CardId, gp.FileId });

            modelBuilder.Entity<GameCategory>()
                .HasKey(gp => new { gp.CategoryId, gp.GameId });

            modelBuilder.Entity<PostAttachment>()
                .HasKey(gp => new { gp.PostId, gp.FileId });
        }
    }
}
